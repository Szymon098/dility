import { Employee } from '../models/employee';

export interface EmployeesWithDate {
    employees: Employee[];
    date: Date;
}