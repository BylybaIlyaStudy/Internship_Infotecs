import { Component, OnInit } from '@angular/core';
import { UserStatistics } from './UserStatistics';
import { Events } from './Events';
import { HTTPService } from './HTTPService'

import * as signalR from '@aspnet/signalr';
import { UsersWithEventsComponent } from './UsersWithEvents.component';

const TIME = 10;

@Component({
    selector: 'my-app',
    templateUrl: `./app.component.html`,
    providers: [ HTTPService ],
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit { 
    users: UserStatistics[] = [];
    userWhoseEventsAreSelected: UserStatistics = null;

    displayedColumnsWithoutEvents: string[] = ['name', 'date', 'version', 'os'];
    displayedColumnsWithEvents: string[] = ['name', 'date'];
    displayedColumnsEvents: string[] = ['name', 'date'];

    displayedEvents: boolean = false;

    constructor(private http: HTTPService){}
    
    connection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:5001/api')
        .build();

    timeLeft: number = 0;
    timer;

    ngOnInit(): void {  
        this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);

        this.connection.on('update statistics', data => {
            console.log('update statistics');
            this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);
        });
        
        this.connection.on('update users', data => {
            console.log('update users');
            this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);
        });

        this.connection.start();
    }

    onChanged(user: UserStatistics){
        if (user == this.userWhoseEventsAreSelected){
            this.displayedEvents = !this.displayedEvents;
        }
        else {
            if (this.userWhoseEventsAreSelected != null){
                this.connection.off('update events ' + this.userWhoseEventsAreSelected.id);
            }

            this.userWhoseEventsAreSelected = user;
            this.http.getEventsForUser(this.userWhoseEventsAreSelected.id).subscribe((data:Events[]) => this.userWhoseEventsAreSelected.events=data);

            this.connection.on('update events ' + this.userWhoseEventsAreSelected.id, data => {
                console.log('update events ' + this.userWhoseEventsAreSelected.id);
                this.http.getEventsForUser(this.userWhoseEventsAreSelected.id).subscribe((data:Events[]) => this.userWhoseEventsAreSelected.events=data);
            });

            this.displayedEvents = true;
        }
    }
}