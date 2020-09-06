import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Employee } from '../models/employee';
import { EmployeesWithDate } from '../dtos/employees-with-date';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HttpEmployeeService {

  readonly _url = 'https://localhost:44333';

  constructor(private http: HttpClient) { }

  getEmployees(): Promise<Employee[]> {
    return this.http.get<Employee[]>(this._url + '/employees').toPromise();
  }

  postEmployeesFile(employeesWithDate: EmployeesWithDate) {
    return this.http.post(this._url + '/employees/files', employeesWithDate).toPromise();
  }

  postEmployee(employee: Employee) {
    return this.http.post(this._url + '/employee', employee).toPromise();
  }

  isEmployeeAdded(employeeId: string): Promise<boolean> {
    return this.http.get<boolean>(this._url + '/employee/is-added/' + employeeId).toPromise();
  }

  private getFixedDate(date: Date): string {
    var _date = new Date(date);
    return _date.getFullYear() + '.' + (_date.getMonth() + 1) + '.' + _date.getDate();
  }
}