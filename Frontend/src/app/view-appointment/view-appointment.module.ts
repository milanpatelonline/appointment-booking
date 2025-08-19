import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ViewAppointmentRoutingModule } from './view-appointment-routing.module';
import { EditAppointmentComponent } from '@app/edit-appointment/edit-appointment.component';
import { LayoutComponent } from '@app/account/layout.component';
import { ViewAppointmentComponent } from './view-appointment.component';
import { EditAppointmentRoutingModule } from '@app/edit-appointment/edit-appointment-routing.module';


@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        ViewAppointmentRoutingModule,
        EditAppointmentRoutingModule
    ],
    declarations: [
        LayoutComponent,
        //ViewAppointmentComponent,
        //EditAppointmentComponent
    ]
})
export class ViewAppointmentModule { }