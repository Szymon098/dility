import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  public onWorkdayDateChange: EventEmitter<Date>;
  private _workdayDate: Date;

  public set workdayDate(value: Date) {
    this._workdayDate = value;
    this.onWorkdayDateChange.emit(value);
  }

  public get workdayDate() { return this._workdayDate; }

  constructor() {
    this.onWorkdayDateChange = new EventEmitter()
  }

}    