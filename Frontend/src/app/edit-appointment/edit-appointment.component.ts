import { Component, OnInit } from '@angular/core';

import { AccountService, AlertService } from '@app/_services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({ templateUrl: 'edit-appointment.component.html' })

export class EditAppointmentComponent implements OnInit {
     form!: FormGroup;
    appointment: any;
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
        debugger;
        this.userType = localStorage.getItem("userRole");
        this.loadStaffMembers();
        var editUserString = localStorage.getItem("editAppointmentRecord");
        this.appointment = JSON.parse(editUserString || '{}');

        this.form = this.formBuilder.group({            
            assignedToId: [this.appointment.assignedToId, Validators.required],
            customerName: [this.appointment.customerName, ],
            appointmentDateTime: [this.appointment.appointmentDateTime,],
            appointmentDate: [this.appointment.appointmentDate,Validators.required],
            appointmentTime:[this.appointment.appointmentTime,Validators.required],
            customerUserId: [this.appointment.customerUserId,Validators.required],
            details: [this.appointment.details,Validators.required],
            createdById: [this.appointment.createdById,],
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
        debugger;
        var appointmentId = this.appointment.id;
        const dateTimeString = `${this.form.value.appointmentDate}T${this.form.value.appointmentTime}`;
        const combinedDateTime = new Date(dateTimeString);
        var userString = localStorage.getItem("user");        
        const user = JSON.parse(userString || '{}');
        //this.form.controls['customerUserId'].setValue(user.userId);
        //this.form.controls['createdById'].setValue(user.userId);
        this.form.controls['appointmentDateTime'].setValue(combinedDateTime);
        
        this.accountService.updateAppointment(this.form.value,appointmentId).subscribe({
            next: (data:any) => {
                data?.isSuccess ? this.alertService.success('Appointment booked successfully') : this.alertService.error(data.errorMessage);
                this.bookingSuccess = true;
                this.bookingError = '';
                this.alertService.clear();
                this.form.reset();
            },
            error: (err) => {
                this.bookingError = 'Failed to book appointment.';
                this.bookingSuccess = false;
            }
        });
    }
}