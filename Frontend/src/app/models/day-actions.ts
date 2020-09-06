import { UsersActions } from './users-actions';

export interface DayActions {
    date: Date;
    usersActions: UsersActions[];
}