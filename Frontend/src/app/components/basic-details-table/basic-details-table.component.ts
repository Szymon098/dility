import { Component, OnInit } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { HttpDetailsService } from '../../services/details.http.service';
import { Statement } from '@angular/compiler';
import { EmployeeDetails } from '../../models/employee-details';
import { EmployeeBasicDetailsOnDay } from '../../models/basic-employee-details-on-day';
import { EmployeeBasicDetails } from '../../models/basic-employee-details';
import { Router, ActivatedRoute } from '@angular/router';
import { NavbarService } from '../../services/navbar.service';

@Component({
  selector: 'basic-details-table',
  styleUrls: ['basic-details-table.component.css'],
  templateUrl: 'basic-details-table.component.html',
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class BasicDetailsTable implements OnInit {
  expandedElement: Statement;
  employeesBasicDetailsOnDay: EmployeeBasicDetailsOnDay;
  dataSource: EmployeeBasicDetails[];
  employeeDetails: EmployeeDetails;
  columnsToDisplay = ['Index', 'Identifier', 'Name', 'Surname', 'Performed Jobs'];
  basicDetailsProperties = ['index', 'employeeId', 'firstName', 'lastName', 'performedJobs'];
  workdayDate: Date;
  errorAlert: string = '';
  displayError: boolean = true;
  displayDatePicker: boolean = false;
  displayLoading: boolean = true;

  constructor(
    private _httpDetailsService: HttpDetailsService,
    private _router: Router,
    private _navbarService: NavbarService,
    private _activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    setTimeout(() =>
      this.loadEmployeesBasicDetails()
      , 500);
  }

  loadEmployeesBasicDetails() {
    var thereAreDataFromLastAddedFile = this._activatedRoute.snapshot.paramMap.get('getDataFromLastAddedFile');

    if (thereAreDataFromLastAddedFile) {
      this.loadEmployeesBasicDetailsFromLastAddedFile();
    }
    else {
      this.loadNewestBasicDetails();
    }
  }

  async loadNewestBasicDetails() {
    // var result = await this._httpDetailsService.getNewestEmployeesBasicDetails().then(result =>
    await this._httpDetailsService.getNewestEmployeesBasicDetails().then(result => {
      this.setVariablesWhenSuccess(result);
    }).catch(err => {
      this.setConnectionError();
    });

    // if (result) {
    //   this.setVariablesWhenSuccess(result);
    // }
    // else {
    //   this.setConnectionError();
    // }

  }

  async loadEmployeesBasicDetailsFromLastAddedFile() {

    const result = await this._httpDetailsService.getEmployeesBasicDetailsFromLastAddedFile();
    if (result)
      this.setVariablesWhenSuccess(result);
    else
      this.setConnectionError();

  }

  async onDateChangeBasicDetailsDate(changedWorkdayDate: Date) {
    const result = await this._httpDetailsService.getAnotherDayBasicDetails(changedWorkdayDate);

    if (result) {
      this.setVariablesWhenSuccess(result);
      this.employeesBasicDetailsOnDay.employeesBasicDetails === null ? this.setEmptyDateError() : '';
    }
    else
      this.setEmptyDateError();
  }

  setEmptyDateError() { // used when day is not saved in database then api returns empty day
    this.employeesBasicDetailsOnDay = null;
    this.displayError = true;
    this.errorAlert = "No data from this day";
  }

  setConnectionError() { //used when there's no connection with api 
    this.errorAlert = 'Server connection issue';
    this.displayDatePicker = false;
    this.displayError = true;
    this.displayLoading = false;

  }

  setNoErrors() { // used when successful received data from api
    this.displayError = false;
    this.displayDatePicker = true;
  }

  async setEmployeeDetails(id: string) {
    const result = await this._httpDetailsService.getEmployeeDetails(id, this.workdayDate);

    result ? this.employeeDetails = result : ''
  }

  setVariablesWhenSuccess(data: EmployeeBasicDetailsOnDay) {
    this.setValuesFromApi(data)
    this._navbarService.workdayDate = data.workdayDate;
    this.setNoErrors();
    this.displayLoading = false;
  }

  setValuesFromApi(result: EmployeeBasicDetailsOnDay) {
    this.employeesBasicDetailsOnDay = result;
    this.dataSource = this.employeesBasicDetailsOnDay.employeesBasicDetails;
    this.workdayDate = this.employeesBasicDetailsOnDay.workdayDate;
  }
}