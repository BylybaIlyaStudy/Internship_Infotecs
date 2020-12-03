import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import * as signalR from '@aspnet/signalr';

import { UserStatistics } from '../Models/UserStatistics';
import { HTTPService } from '../HTTPService'

@Component({
    selector: 'Users-component',
    templateUrl: './Users.component.html',
    styleUrls: ['./Users.component.css']
})

export class UsersComponent implements OnInit, OnDestroy { 
    displayedColumns: string[] = ['name', 'date', 'version', 'os'];

    users: UserStatistics[];
    selectedUserID: string;

    connection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:5001/api')
        .build();

    constructor(private http: HTTPService, private router: Router){}

    ngOnInit(): void{
        this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);

        this.connection.on('update statistics', data => {
            this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);
        });
        
        this.connection.on('update users', data => {
            this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);
        });

        this.connection.start();
    }

    ngOnDestroy(): void{
        this.connection.off('update statistics');
        this.connection.off('update users');

        this.connection.stop();
    }

    SetUser(user){
        this.selectedUserID = user.id;

        this.router.navigate(['/users', user.id]);
    }
}