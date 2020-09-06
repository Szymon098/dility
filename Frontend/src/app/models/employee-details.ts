import { ActionType } from './action-type';

export interface EmployeeDetails {
    mostPerformedType: ActionType;
    leastPerformedType: ActionType;
    efficiency: number;
    performedMinutes: number;
}