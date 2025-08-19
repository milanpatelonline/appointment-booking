import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService, AlertService } from '@app/_services';

@Component({ templateUrl: 'add-edit.component.html' })
export class AddEditComponent implements OnInit {
    form!: FormGroup;
    id?: string;
    title!: string;
    loading = false;
    submitting = false;
    submitted = false;
    userResult: any;
    isDisabled = true;
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    userRoles = [
        { name: 'Admin', id: 1 },
        { name: 'Staff', id: 2 },
        { name: 'Aplicant', id: 3 }
    ];

    ngOnInit() {        
        var editUserString = localStorage.getItem("editUser");
        this.userResult = JSON.parse(editUserString || '{}');

        // this.form = this.formBuilder.group({
        //     firstName: ['',],
        //     lastName: ['', ],
        //     email: ['', ],
        //     role: ['', ]
        // });

        var roleId = this.userRoles.find(role => role.name === this.userResult.userRole)?.id || 1;
        
        this.form = this.formBuilder.group({
            firstName: [this.userResult.firstName, Validators.required],
            lastName: [this.userResult.lastName, Validators.required],
            email: [this.userResult.email, [Validators.required, Validators.email]],
            role: [this.userResult.userRole, Validators.required],
            roleId: [roleId, Validators.required],
        });

        //this.form.patchValue(this.userResult);
        this.title = 'Update User';

    }

    //convenience getter for easy access to form fields
    get f() { return this.form.controls; }

    onSubmit() {
        debugger;
        if (this.form.invalid) {
            return;
        }
        this.submitting = true;
        

        var rolename = this.userRoles.find(role => role.id === this.form.value.roleId)?.name || 'staff';
        this.form.controls['role'].setValue(rolename);
        this.accountService.assignRole(this.form.value).subscribe({
            next: (data:any) => {
                data?.isSuccess ? this.alertService.success('User successfully updated ') : this.alertService.error(data.errorMessage);
                setTimeout(() => { 
                    this.alertService.clear();
                    this.router.navigateByUrl('/users'); 
                }, 3000);
                this.submitting = false;                                
            },
            error: (err) => {
                this.alertService.error("Failed to update user.");
                this.submitting = false;
            }
        });
    }

    
}