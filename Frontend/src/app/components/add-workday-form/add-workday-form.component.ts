import { Component, OnInit, Inject, ɵEMPTY_ARRAY } from '@angular/core';
import { Employee } from '../../models/employee';
import { MatDialog } from '@angular/material/dialog';
import { HttpEmployeeService } from '../../services/employee.http.service'
import { UsersActions } from '../../models/users-actions';
import { DayActions } from '../../models/day-actions';
import { ActionType } from '../../models/action-type';
import { Action } from '../../models/action';
import { MatSelectChange } from '@angular/material/select';
import { EmployeesWithDate } from '../../dtos/employees-with-date';
import { Router } from '@angular/router';
import { EmployeesDialogTable } from '../employees-dialog/employee-list.component';
import { DateDialog } from '../date-dialog/date-dialog';
import { HttpWorkdayService } from '../../services/workday.http.service';


@Component({
  selector: 'app-add-workday-form',
  templateUrl: './add-workday-form.component.html',
  styleUrls: ['./add-workday-form.component.css']
})

export class AddWorkdayFormComponent implements OnInit {
  allEmployees: Employee[];
  choosenEmployees: Employee[] = [];
  usersActions: UsersActions[] = [];
  dayActions: DayActions;
  ActionType = ActionType;
  disableSubmit = true;
  displayError = true;
  error: string;
  displayLoading: boolean = true;
  expandedEmployee: Employee;

  constructor(
    private _dialog: MatDialog,
    private _employeesHttpService: HttpEmployeeService,
    private _workdayHttpService: HttpWorkdayService,
    private _router: Router,
  ) { }

  async ngOnInit(): Promise<void> {
    this.loadEmployees();
  }

  loadEmployees() {
    setTimeout(() =>
      this._employeesHttpService.getEmployees().then(result => {
        this.displayError = false;
        this.allEmployees = result;
        this.displayLoading = false;
      }).catch(err => {
        this.displayError = true;
        this.displayLoading = false;
        this.error = 'Server connection issue'
      })
      , 500);
  }

  removeEmployee(employee: Employee) {
    this.choosenEmployees = this.choosenEmployees.filter(e => e !== employee);
    this.allEmployees.push(employee);
    this.removeUserAction(employee);
    this.checkSubmitAccesibility();
  }

  removeUserAction(employee: Employee) {
    this.usersActions = this.usersActions.filter(ua => ua.employeeId.toString() !== employee.employeeId);
    this.checkSubmitAccesibility();
  }

  addUserActions(employee: Employee) {
    let userActions: UsersActions = {
      employeeId: employee.employeeId,
      actions: []
    }

    this.usersActions.push(userActions);
    this.checkSubmitAccesibility();
  }

  removeActionFromUserActionsOnPosition(employee: Employee, position: number) {
    let userActions = this.getUserActionsOfEmployee(employee);
    let actions = userActions.actions;
    actions.splice(position, 1);
    this.checkSubmitAccesibility();
  }

  onPlusButtonClick() {
    const dialogRef = this._dialog.open(EmployeesDialogTable,
      {
        data: this.allEmployees,
      });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.allEmployees = this.allEmployees.filter(e => e !== result);
        this.choosenEmployees.push(result);
        this.expandedEmployee = result;
        this.addUserActions(result);
      }
    });
  }

  setExpandedEmployee(employee: Employee) {
    this.expandedEmployee = employee;
  }
  selected(event: MatSelectChange) {
    if (this.expandedEmployee) {
      let userActions = this.usersActions.find(ua => ua.employeeId.toString() == this.expandedEmployee.employeeId)
      let action: Action = {
        type: event.value,
      }
      userActions.actions.push(action);
      event.source.value = null;
      this.checkSubmitAccesibility();
    }
  }

  clearEmployeeUserActions(employee) {
    let currentActions = this.getUserActionsOfEmployee(employee);
    currentActions.actions = [];
    this.checkSubmitAccesibility();
  }
  getUserActionsOfEmployee(employee: Employee): UsersActions {
    return this.usersActions.find(ua => ua.employeeId.toString() == employee.employeeId)
  }

  createDayActions(date: Date) {
    let dayActions: DayActions = {
      date: date,
      usersActions: this.usersActions,
    }
    this.dayActions = dayActions;
    this.checkSubmitAccesibility();
  }

  onSubmit() {
    this.openDateDialog();

  }

  async sendEmployeesWithDate() {
    const employeesWithDate: EmployeesWithDate = {
      date: this.dayActions.date,
      employees: this.choosenEmployees,
    }

    await this._employeesHttpService.postEmployeesFile(employeesWithDate);
    await this._workdayHttpService.postWorkday(this.dayActions);

    setTimeout(() => {
      this._router.navigate(['/details', { getDataFromLastAddedFile: true }]);
    }, 800)
  }

  openDateDialog(): void {
    const dialogRef = this._dialog.open(DateDialog, {
      width: '400px',
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result != null) {
        this.createDayActions(result);
        this.sendEmployeesWithDate();
      }
      else {
        alert("Choose proper date");
      }
    });
  }

  checkSubmitAccesibility() {
    if (this.usersActions.length > 0 && this.checkIfEveryUserActionsHasActions()) {
      this.disableSubmit = false;
      return;
    }
    this.disableSubmit = true;
  }

  checkIfEveryUserActionsHasActions(): boolean {
    for (let i = 0; i < this.usersActions.length; i++)
      if (this.usersActions[i].actions.length == 0)
        return false

    return true;
  }
}