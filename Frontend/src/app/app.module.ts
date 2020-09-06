import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from "@angular/common";
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ExtendedDetailsRow } from './components/basic-details-table/extended-details-row/extended-details-row.component';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HttpEmployeeService } from './services/employee.http.service';
import { HttpWorkdayService } from './services/workday.http.service';
import { HttpDetailsService } from './services/details.http.service';
import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './materials.module';
import { DatePicker } from './components/date-picker/date-picker.component';
import { DatePipe } from '@angular/common';
import { AppComponent } from './app.component';
import { BasicDetailsTable } from './components/basic-details-table/basic-details-table.component';
import { AddWorkdayFormComponent } from './components/add-workday-form/add-workday-form.component';
import { AddEmployeesComponent } from './components/add-employees/add-employees.component';
import { NavbarService } from './services/navbar.service';
import { EmployeesDialogTable } from './components/employees-dialog/employee-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ExtendedDetailsRow,
    NavbarComponent,
    DatePicker,
    BasicDetailsTable,
    AddWorkdayFormComponent,
    EmployeesDialogTable,
    AddEmployeesComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule,
    MaterialModule,
    CommonModule,
  ],
  providers: [
    HttpEmployeeService,
    DatePipe,
    NavbarService,
    HttpWorkdayService,
    HttpDetailsService,
  ],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
