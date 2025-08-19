import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService, AlertService } from '@app/_services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({ templateUrl: 'appointment.component.html' })
export class AppointmentComponent implements OnInit {
    form!: FormGroup;
    appointment?: any[];
    loading = false;
    submitting = false;
    userType: any;
    staffMembers: any[] = [];
    bookingSuccess = false;
    bookingError = '';
    constructor(private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService) { }

    ngOnInit() {
       this.userType = localStorage.getItem("userRole")
        this.loadStaffMembers();
        this.form = this.formBuilder.group({
            assignedToId: ['', Validators.required],
            appointmentDateTime: ['',],
            appointmentDate: ['', Validators.required],
            appointmentTime: ['', Validators.required],
            customerUserId: ['',],
            details: ['', Validators.required],
            createdById: ['',]
        });
    }

    loadStaffMembers() {
        this.accountService.getStaffMembers().subscribe({
            next: (data) => (
                this.staffMembers = data.result || []),
            error: (err) => console.error('Error loading staff', err)
        });        
    }

    onSubmit() {
        debugger
        const dateTimeString = `${this.form.value.appointmentDate}T${this.form.value.appointmentTime}`;
        const combinedDateTime = new Date(dateTimeString).toLocaleString();

        const dt = new Date(combinedDateTime).toISOString()

        var userString = localStorage.getItem("user");        
        const user = JSON.parse(userString || '{}');
        this.form.controls['customerUserId'].setValue(user.userId);
        this.form.controls['createdById'].setValue(user.userId);
        this.form.controls['appointmentDateTime'].setValue(dt);
        this.alertService.clear();
        this.accountService.bookAppointment(this.form.value).subscribe({
            next: (data: any) => {
                //debugger
                if (data.isSuccess) {
                    this.alertService.success('Appointment booked successfully')
                    this.bookingSuccess = true;
                    this.bookingError = '';
                    this.form.reset();
                }
                else {
                    this.bookingError = data.errorMessage || 'Failed to book appointment.';
                    this.bookingSuccess = false;
                    this.alertService.error(data.errorMessage);
                }
            },
            error: (err) => {
                this.bookingError = 'Failed to book appointment.';
                this.bookingSuccess = false;
            }
        });
    }
}