import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { AuthGuard } from './_helpers';
import { AppointmentComponent } from './appointment/appointment.component';
import { ViewAppointmentComponent } from './view-appointment/view-appointment.component';
import { EditAppointmentComponent } from './edit-appointment/edit-appointment.component';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const usersModule = () => import('./users/users.module').then(x => x.UsersModule);
//const appointmentModule = () => import('./appointment/appointment.module').then(x => x.AppointmentModule);
//const viewAppointmentModule = () => import('./view-appointment/view-appointment.module').then(x => x.ViewAppointmentModule);

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'users', loadChildren: usersModule, canActivate: [AuthGuard] },
    { path: 'account', loadChildren: accountModule },
    //{ path: 'view-appointment', loadChildren: viewAppointmentModule, canActivate: [AuthGuard] },
    //{ path: 'appointment', loadChildren: appointmentModule, canActivate: [AuthGuard] },
    { path: 'appointment', component: AppointmentComponent, canActivate: [AuthGuard] },
    { path: 'view-appointment', component: ViewAppointmentComponent, canActivate: [AuthGuard] },
    { path: 'editAppointment', component: EditAppointmentComponent, canActivate: [AuthGuard] },
    
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }