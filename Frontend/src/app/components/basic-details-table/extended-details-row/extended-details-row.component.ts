import { Component, Input, OnInit } from '@angular/core';
import { EmployeeDetails } from '../../../models/employee-details'
import { ActionType } from '../../../models/action-type';

@Component({
  selector: 'app-extended-details-row',
  templateUrl: './extended-details-row.component.html',
  styleUrls: ['./extended-details-row.component.css']
})
export class ExtendedDetailsRow implements OnInit {
  @Input() employeeDetails: EmployeeDetails;
  actionType = ActionType;

  constructor() { }

  ngOnInit() {

  }
}
