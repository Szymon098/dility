import { Component, OnInit } from '@angular/core';
import { Employee } from '../../models/employee';
import { HttpEmployeeService } from '../../services/employee.http.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-employees',
  templateUrl: './add-employees.component.html',
  styleUrls: ['./add-employees.component.css']
})
export class AddEmployeesComponent implements OnInit {
  name: string = '';
  surname: string = '';
  employeeId: number = null;
  isNameEmpty = false;
  isSurnameEmpty = false;
  isIdInvalid = false;
  displayEmployeeExistError = false;

  constructor(
    private _employeesHttpService: HttpEmployeeService,
    private _snackBar: MatSnackBar,

  ) { }

  ngOnInit(): void { }

  async onSubmitClick() {
    if (this.isDataValid()) {
      let employee: Employee = {
        firstName: this.name,
        lastName: this.surname,
        employeeId: this.employeeId.toString(),
      }

      if (await this.isEmployeeAdded()) {
        this.displayEmployeeExistError = true;
        return;
      } else {

        this.displayEmployeeExistError = false;
        this._employeesHttpService.postEmployee(employee);
        this.openSnackBar();

        setTimeout(() => {
          location.reload();
        }, 1600)
      }
    }
  }

  async isEmployeeAdded(): Promise<boolean> {
    const result = await this._employeesHttpService.isEmployeeAdded(this.employeeId.toString());

    return result;
  }

  isDataValid(): boolean {
    if (this.name == '')
      this.isNameEmpty = true;
    if (this.name != '')
      this.isNameEmpty = false;

    if (this.surname == '')
      this.isSurnameEmpty = true;
    if (this.surname != '')
      this.isSurnameEmpty = false;

    if (this.employeeId == null || this.employeeId.toString().length != 5)
      this.isIdInvalid = true;
    if (this.employeeId && this.employeeId.toString().length == 5)
      this.isIdInvalid = false;

    if (this.isNameEmpty || this.isSurnameEmpty || this.isIdInvalid)
      return false;

    return true;
  }

  openSnackBar() {
    this._snackBar.open("EmployeeAdded", "Close", {
      duration: 2000,
    });
  }
}
