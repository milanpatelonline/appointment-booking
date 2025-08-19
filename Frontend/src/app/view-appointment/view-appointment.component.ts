import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService, AlertService } from '@app/_services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({ templateUrl: 'view-appointment.component.html' })
export class ViewAppointmentComponent implements OnInit {
     appointments?: any[];
     userType:any;

    constructor(private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService) { }

    ngOnInit() {
       this.userType = localStorage.getItem("userRole")
        this.loadAppointments();
        
    }

    loadAppointments() {
        this.accountService.getAppointments().subscribe({
            next: (data) => (
                this.appointments = data.result || []),
            error: (err) => console.error('Error loading staff', err)
        });        
    }

    onSubmit() {
    }

    deleteUser(id: string) {        
        if (confirm("Are you sure to Delete this appointment?")) {
            this.accountService.deleteAppointment(id).pipe(first()).subscribe({
                next: (data:any) => {
                    data?.isSuccess ? this.alertService.success('Appointment deleted successfully') : this.alertService.error(data.errorMessage);
                    this.loadAppointments();                   
                },
                error: error => {
                    this.alertService.error(error);
                }
            });
        }
    }

    editAppointment(appointment: any) {
         localStorage.setItem("editAppointmentRecord", JSON.stringify(appointment));
        this.router.navigate(['editAppointment']);
    }

    getFormattedDate(date: string): string {
        //const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' };
        //return new Date(date).toLocaleDateString('en-US', options);

        const dt = new Date(date);
        return dt.toLocaleString();
    }
}