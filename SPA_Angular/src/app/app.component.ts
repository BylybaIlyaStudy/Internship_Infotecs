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
        const connection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:5001/api')
            .build();

        connection.on('send', data => {
            console.log("update");
            this.users = this.http.getData();
            this.timer = setInterval(() => { this.users = this.http.getData(); }, 1000);
        });
        
        connection.start()
            .then(() => connection.invoke('send', 'hi'))
            .catch(err => console.log('Error while starting connection: ' + err));

        //this.users = this.http.getData();
        //console.log(this.users);
        //this.timer = setInterval(() => {
        //    if(this.timeLeft < TIME) {
        //        this.timeLeft++;
        //        //console.log(this.timeLeft);
        //    } else {
        //        this.users = this.http.getData();
        //        this.timeLeft = 0;
        //        console.log("time");
        //    }
        //}, 1000);
    }

    //ngOnDestroy(){
    //    clearInterval(this.timer);
    //}

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