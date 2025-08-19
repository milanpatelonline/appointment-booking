import { Component, OnInit } from '@angular/core';

import { User } from '@app/_models';
import { AccountService } from '@app/_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {
    
    user: User | null;
    userType: any;

    constructor(private accountService: AccountService) {
        this.user = this.accountService.userValue;
    }

     ngOnInit(): void {
        //debugger;
        this.userType = localStorage.getItem("userRole");
    } 
}