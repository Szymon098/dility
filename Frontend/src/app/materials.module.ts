import { NgModule } from '@angular/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatMenuModule } from '@angular/material/menu';
import { MatTreeModule } from '@angular/material/tree';

@NgModule({
    imports: [
        MatListModule,
    ],
    exports: [
        MatDatepickerModule,
        MatFormFieldModule,
        MatIconModule,
        MatNativeDateModule,
        MatInputModule,
        MatSelectModule,
        MatTableModule,
        MatButtonModule,
        MatListModule,
        MatDividerModule,
        MatExpansionModule,
        MatDialogModule,
        MatProgressSpinnerModule,
        MatSnackBarModule,
        MatMenuModule,
        MatTreeModule,
    ],
})

export class MaterialModule { exports }
