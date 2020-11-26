import { Component, OnInit } from '@angular/core';
import { UserStatistics } from './UserStatistics';
import { HTTPService } from './HTTPService'

import * as signalR from '@aspnet/signalr';

const TIME = 10;

@Component({
    selector: 'my-app',
    templateUrl: `./app.component.html`,
    providers: [ HTTPService ],
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit { 
    users: UserStatistics[] = [];
    userWhoseEventsAreSelected: UserStatistics;

    displayedColumnsWithoutEvents: string[] = ['name', 'date', 'version', 'os'];
    displayedColumnsWithEvents: string[] = ['name', 'date'];
    displayedColumnsEvents: string[] = ['name', 'date'];

    displayedEvents: boolean = false;

    constructor(private http: HTTPService){}
    
    timeLeft: number = 0;
    timer;

    ngOnInit(): void {  
        this.http.getData().subscribe((data:UserStatistics[]) => this.users=data);

        const connection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:5001/api')
            .build();

        connection.on('update', data => {
            this.http.getData().subscribe((data:UserStatistics[]) => this.users=data);
        });
        
        connection.start();
    }

    onChanged(user: UserStatistics){
        if (user == this.userWhoseEventsAreSelected){
            this.displayedEvents = !this.displayedEvents;
        }
        else {
            this.userWhoseEventsAreSelected = user;
            this.displayedEvents = true;
        }
    }
}