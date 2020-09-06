import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { DatePipe } from '@angular/common';
/** @title Datepicker with custom icon */
@Component({
  selector: 'date-picker',
  styleUrls: ['date-picker.component.css'],
  templateUrl: 'date-picker.component.html',
})
export class DatePicker implements OnInit {
  workdayDate: Date;
  @Output() changedWorkdayDate = new EventEmitter<Date>();
  @Input() labelDate: Date = null;

  constructor() { }

  ngOnInit() { }

  getLabelValue(): string {

    if (this.labelDate == null)
      return "Choose a date"

    var _date = new Date(this.labelDate);
    var datePipe = new DatePipe('en-US');

    if (_date.getFullYear() == 1)
      return "Choose a date"


    return datePipe.transform(_date, 'dd/MM/yyyy');
  }

  setWorkdayDate() {
    this.changedWorkdayDate.emit(this.workdayDate);
  }
}
