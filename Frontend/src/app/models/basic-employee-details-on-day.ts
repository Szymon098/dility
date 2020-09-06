import { EmployeeBasicDetails } from './basic-employee-details';

export interface EmployeeBasicDetailsOnDay {
  workdayDate: Date;
  employeesBasicDetails: EmployeeBasicDetails[];
}