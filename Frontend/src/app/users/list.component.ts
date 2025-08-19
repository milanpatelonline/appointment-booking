import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '@app/_services';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    users?: any[];
    userType:any;

    constructor(private accountService: AccountService,  
        private route: ActivatedRoute,
        private router: Router,) {}

    ngOnInit() {
        //debugger;
        this.userType=localStorage.getItem("userRole")
        this.accountService.getAll().pipe(first()).subscribe((users:any) => this.users = users.result);        
    }

    editUser(user:any){
        localStorage.setItem("editUser", JSON.stringify(user));
        this.router.navigate(['edituser'], { relativeTo: this.route });        
    }

    // deleteUser(id: string) { 
    //     //debugger       
    //     if(confirm("Are you sure to Active/Deactive ")) {
    //         const user = this.users!.find(x => x.userId === id);
    //         if(user.isActive){
    //         user.isActive = false;
    //         this.accountService.delete(id)
    //             .pipe(first())
    //             .subscribe();
    //         }
    //         else{
    //         user.isActive=true
    //         //this.accountService.active(id).pipe(first()).subscribe();
    //         }
    //       }
    // }
}