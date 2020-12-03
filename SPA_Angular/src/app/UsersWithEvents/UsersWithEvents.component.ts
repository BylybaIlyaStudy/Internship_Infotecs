import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import * as _ from 'lodash'; 
import * as signalR from '@aspnet/signalr';

import { UserStatistics } from '../Models/UserStatistics';
import { HTTPService } from '../HTTPService';
import { Events } from '../Models/Events';

@Component({
    selector: 'UsersWithEvents-component',
    templateUrl: './UsersWithEvents.component.html',
    styleUrls: ['./UsersWithEvents.component.css']
})

export class UsersWithEventsComponent implements OnInit, OnDestroy { 
    displayedColumnsUsers: string[] = ['name', 'date'];
    displayedColumnsEvents: string[] = ['name', 'date', 'description'];

    users: UserStatistics[] = [];
    user: UserStatistics = new UserStatistics;

    connection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:5001/api')
        .build();

    private subscription: Subscription;
    constructor(private http: HTTPService, private router: Router, private activateRoute: ActivatedRoute){
        this.subscription = activateRoute.params.subscribe(params=>this.selectedUserID=params['id']);
    }

    ngOnInit(): void{    
        this.http.getUsersList().subscribe((data:UserStatistics[]) => {
            this.users = data;

            this.user = this.users.find(x => x.id == this.selectedUserID);
            this.http.getEventsForUser(this.selectedUserID).subscribe((data:Events[]) => {
                this.user.events = data;
                this.displayedEvents = this.user.events;
                this.enteringDescription = false;
            });
        });

        this.connection.on('update statistics', data => {
            this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);
        });
        
        this.connection.on('update users', data => {
            this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);
        });

        this.connection.on('update events ' + this.selectedUserID, data => {
            this.http.getEventsForUser(this.selectedUserID).subscribe((data:Events[]) => {
                this.user.events = data;
                this.displayedEvents = data;
            });
        });

        this.connection.start();
    }

    ngOnDestroy(): void{
        this.connection.off('update statistics');
        this.connection.off('update users');
        this.connection.off('update events ' + this.selectedUserID);

        this.connection.stop();
    }

    setUser(user) {
        this.connection.off('update events ' + this.selectedUserID);

        this.selectedUserID = user.id;

        this.router.navigate(['/users', user.id]);

        this.user = this.users.find(x => x.id == this.selectedUserID);
        this.http.getEventsForUser(this.selectedUserID).subscribe((data:Events[]) => {
            this.user.events = data;
            this.displayedEvents = this.user.events;
            this.enteringDescription = false;
        });

        this.connection.on('update events ' + this.selectedUserID, data => {
            this.http.getEventsForUser(this.selectedUserID).subscribe((data:Events[]) => {
                this.user.events = data;
                this.displayedEvents = data;
            });
        });
    }

    selectedUserID: string;
    enteringDescription: boolean = false;
    displayedEvents: Events[] = [];
    
    createDescription() {
        this.http.createEventsDescription(this.displayedEvents);

        this.changeEnteringDescription();
    }

    changeEnteringDescription() {
        this.enteringDescription = !this.enteringDescription;

        if (this.enteringDescription) {
            this.displayedColumnsEvents = ['name', 'description'];
            this.displayedEvents = _.uniqBy(this.user.events, 'name');
        }
        else {
            this.displayedColumnsEvents = ['name', 'date', 'description'];
            this.displayedEvents = this.user.events;
        }
    }

    deleteEvents() {
        this.http.deleteEvents(this.selectedUserID);
    }
}