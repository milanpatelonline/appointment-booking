import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppointmentComponent } from './appointment.component';
import { LayoutComponent } from '@app/account/layout.component';
import { EditAppointmentComponent } from '@app/edit-appointment/edit-appointment.component';
//import { AddEditComponent } from '@app/users/add-edit.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: '', component: AppointmentComponent },
            { path: 'editAppointment', component: EditAppointmentComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AppointmentRoutingModule { }