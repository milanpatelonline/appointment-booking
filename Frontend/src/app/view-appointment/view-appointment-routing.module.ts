import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from '@app/account/layout.component';
import { ViewAppointmentComponent } from './view-appointment.component';
import { EditAppointmentComponent } from '@app/edit-appointment/edit-appointment.component';

const routes: Routes = [
    {
        path: '', component: ViewAppointmentComponent,
        children: [
            { path: '', component: ViewAppointmentComponent },
            { path: 'view-appointment', component: ViewAppointmentComponent },
            //{ path: 'editAppointment/:id', component: EditAppointmentComponent },
            //{ path: 'view-appointment/editAppointment', component: EditAppointmentComponent }, 
             
                     
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ViewAppointmentRoutingModule { }