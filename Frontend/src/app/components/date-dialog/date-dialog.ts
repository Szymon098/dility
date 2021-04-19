import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpWorkdayService } from '../../services/workday.http.service';
@Component({
    selector: 'date-dialog',
    templateUrl: 'date-dialog.html',
    styleUrls: ['./date-dialog.css']

})
export class DateDialog {
    date: Date = null;
    disableSaveButton: boolean = true;
    displayDateError: boolean = false;
    dayNote: string = '';
    constructor(
        private _workdayHttpService: HttpWorkdayService,
        public dialogRef: MatDialogRef<DateDialog>,
        @Inject(MAT_DIALOG_DATA) public data: Date) { }

    onCancel(): void {
        this.dialogRef.close();
    }

    async onDateChangeCreateDayActions(date: Date) {
        if (date > new Date()) {
            this.displayDateError = true;
            this.dayNote = "That day has not yet taken place";

            return;
        }
        const result = await this._workdayHttpService.isWorkdayExist(date);
        this.displayDateError = result;

        if (this.displayDateError) {
            this.dayNote = "The day has already been added";
            return;
        }

        this.date = date;
        this.disableSaveButton = false;
    }
}
