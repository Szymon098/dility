import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { BasicDetailsTable } from './components/basic-details-table/basic-details-table.component';
import { AddWorkdayFormComponent } from './components/add-workday-form/add-workday-form.component';
import { AddEmployeesComponent } from './components/add-employees/add-employees.component';

const routes: Routes = [
    { path: '', pathMatch: "full", redirectTo: '/about' },
    { path: 'about', component: AboutComponent },
    { path: 'details', component: BasicDetailsTable },
    { path: 'add-workday', component: AddWorkdayFormComponent },
    { path: 'add-employees', component: AddEmployeesComponent },
    // { path: '**', redirectTo: '/about', pathMatch: "full" },
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes),
    ],
    exports: [
        RouterModule,
    ],
})
export class AppRoutingModule { exports }