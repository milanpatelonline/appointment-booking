import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '@app/account/layout.component';
import { EditAppointmentComponent } from './edit-appointment.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: '', component: EditAppointmentComponent },            
            //{ path: 'edituser', component: AddEditComponent },
            //{ path: 'view/:id', component: ViewComponent },            
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class EditAppointmentRoutingModule { }