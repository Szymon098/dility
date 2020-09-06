import { Component, OnInit, Inject } from '@angular/core';
import { Employee } from '../../models/employee';
import { MatTableDataSource } from '@angular/material/table';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';


@Component({
    selector: 'employees-list',
    templateUrl: 'employees-list.html',
    styleUrls: ['./employee-list.css']
})

export class EmployeesDialogTable implements OnInit {
    displayedColumns: string[] = ['FirstName', 'LastName', 'Identifier', ' '];
    employees: Employee[];
    dataSource: MatTableDataSource<Employee>;

    constructor(
        private dialogRef: MatDialogRef<EmployeesDialogTable>,
        @Inject(MAT_DIALOG_DATA) private _receivedData: Employee[],
    ) { }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();
    }

    ngOnInit() {
        this.dataSource = new MatTableDataSource(this._receivedData)
    }

    getChoosenEmployee(employee: Employee) {
        this.dialogRef.close(employee);
    }
}

