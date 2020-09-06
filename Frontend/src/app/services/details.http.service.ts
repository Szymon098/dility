import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EmployeeBasicDetailsOnDay } from '../models/basic-employee-details-on-day';
import { EmployeeDetails } from '../models/employee-details';


@Injectable({
    providedIn: 'root'
})
export class HttpDetailsService {

    readonly _url = 'https://localhost:44333/details';

    constructor(private http: HttpClient) { }

    getNewestEmployeesBasicDetails(): Promise<EmployeeBasicDetailsOnDay> {
        return this.http.get<EmployeeBasicDetailsOnDay>(this._url + '/newest').toPromise();
    }

    getEmployeesBasicDetailsFromLastAddedFile(): Promise<EmployeeBasicDetailsOnDay> {
        return this.http.get<EmployeeBasicDetailsOnDay>(this._url + '/newest-added-file').toPromise();
    }

    getEmployeeDetails(id: string, date: Date): Promise<EmployeeDetails> {
        return this.http.get<EmployeeDetails>(this._url + '/employee/' + id + '/' + this.getFixedDate(date)).toPromise();
    }

    getAnotherDayBasicDetails(date: Date): Promise<EmployeeBasicDetailsOnDay> {
        return this.http.get<EmployeeBasicDetailsOnDay>(this._url + '/' + this.getFixedDate(date)).toPromise();
        // .pipe(tap(console.log))
    }

    private getFixedDate(date: Date): string {
        var _date = new Date(date);
        return _date.getFullYear() + '.' + (_date.getMonth() + 1) + '.' + _date.getDate();
    }

}