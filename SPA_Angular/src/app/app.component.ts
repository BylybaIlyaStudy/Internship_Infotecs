import { Component, OnInit } from '@angular/core';
import { UserStatistics } from './UserStatistics';
import { Events } from './Events';
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
    userWhoseEventsAreSelected: UserStatistics = null;

    displayedColumnsWithoutEvents: string[] = ['name', 'date', 'version', 'os'];
    displayedColumnsWithEvents: string[] = ['name', 'date'];
    displayedColumnsEvents: string[] = ['name', 'date', 'description'];

    displayedEvents: boolean = false;

    selectedRowID: string;

    constructor(private http: HTTPService){}
    
    connection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:5001/api')
        .build();

    timeLeft: number = 0;
    timer;

    ngOnInit(): void {  
        this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);

        this.connection.on('update statistics', data => {
            this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);
        });
        
        this.connection.on('update users', data => {
            this.http.getUsersList().subscribe((data:UserStatistics[]) => this.users=data);
        });

        this.connection.start();
    }

    onSetUser(user: UserStatistics){
        if (user == this.userWhoseEventsAreSelected){
            
            if (this.displayedEvents) {
                this.displayedEvents = false;
                this.selectedRowID = '';
            }
            else { 
                this.displayedEvents = true;
                this.selectedRowID = user.id;
            }
        }
        else {
            if (this.userWhoseEventsAreSelected != null){
                this.selectedRowID = '';
                this.connection.off('update events ' + this.userWhoseEventsAreSelected.id);
            }

            this.selectedRowID = user.id;

            this.userWhoseEventsAreSelected = user;
            this.http.getEventsForUser(this.userWhoseEventsAreSelected.id).subscribe((data:Events[]) => this.userWhoseEventsAreSelected.events=data);

            this.connection.on('update events ' + this.userWhoseEventsAreSelected.id, data => {
                this.http.getEventsForUser(this.userWhoseEventsAreSelected.id).subscribe((data:Events[]) => this.userWhoseEventsAreSelected.events=data);
            });

            this.displayedEvents = true;
        }
    }

    onCreateDescription(user: UserStatistics) {
        this.http.createEventsDescription(user);
    }

    onselectedRowIDChange(id) {
        this.selectedRowID = id;
    }

    onDeleteEvents(user: UserStatistics){
        this.http.deleteEvents(user.id);
        //this.http.getEventsForUser(this.userWhoseEventsAreSelected.id).subscribe((data:Events[]) => this.userWhoseEventsAreSelected.events=data);
    }
}
