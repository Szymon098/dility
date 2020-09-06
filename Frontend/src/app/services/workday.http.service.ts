import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DayActions } from '../models/day-actions';

@Injectable({
    providedIn: 'root'
})
export class HttpWorkdayService {

    readonly _url = 'https://localhost:44333/workday';

    constructor(private http: HttpClient) { }

    postWorkday(dayActions: DayActions) {
        return this.http.post(this._url + '/actions', dayActions).toPromise();
    }

    isWorkdayExist(date: Date): Promise<boolean> {
        return this.http.get<boolean>(this._url + '/is-exist/' + this.getFixedDate(date)).toPromise();
    }

    private getFixedDate(date: Date): string {
        var _date = new Date(date);
        return _date.getFullYear() + '.' + (_date.getMonth() + 1) + '.' + _date.getDate();
    }
}