// routing
import { AppRoutingModule } from './app-routing.module';

//modules
import { MaterialModule } from './materials.module';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from "@angular/common";
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// utilities
import { DatePipe } from '@angular/common';

// services
import { NavbarService } from './services/navbar.service';
import { HttpEmployeeService } from './services/employee.http.service';
import { HttpWorkdayService } from './services/workday.http.service';
import { HttpDetailsService } from './services/details.http.service';

// components
import { AppComponent } from './app.component';
import { AddWorkdayFormComponent } from './components/add-workday-form/add-workday-form.component';
import { BasicDetailsTable } from './components/basic-details-table/basic-details-table.component';
import { ExtendedDetailsRow } from './components/basic-details-table/extended-details-row/extended-details-row.component';
import { AddEmployeesComponent } from './components/add-employees/add-employees.component';
import { EmployeesDialogTable } from './components/employees-dialog/employee-list.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { AboutComponent } from './components/about/about.component';
import { DatePicker } from './components/date-picker/date-picker.component';

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
    AboutComponent,
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
