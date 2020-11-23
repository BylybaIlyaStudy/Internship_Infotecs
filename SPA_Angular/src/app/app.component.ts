import { Component } from '@angular/core';
import { UserStatistics } from './UserStatistics';
import { HTTPService } from './HTTPService'

const TIME = 10;

@Component({
    selector: 'my-app',
    templateUrl: `./app.component.html`,
    providers: [ HTTPService ],
    styleUrls: ['./app.component.css']
})

export class AppComponent { 
    users: UserStatistics[] = [];
    userWhoseEventsAreSelected: UserStatistics;

    displayedColumnsWithoutEvents: string[] = ['name', 'date', 'version', 'os'];
    displayedColumnsWithEvents: string[] = ['name', 'date'];
    displayedColumnsEvents: string[] = ['name', 'date'];

    displayedEvents: boolean = false;

    constructor(private http: HTTPService){}
    
    timeLeft: number = 0;
    timer;

    ngOnInit(){
        this.users = this.http.getData();
        console.log(this.users);
        this.timer = setInterval(() => {
            if(this.timeLeft < TIME) {
                this.timeLeft++;
                console.log(this.timeLeft);
            } else {
                this.users = this.http.getData();
                this.timeLeft = 0;
            }
        }, 1000);
    }

    ngOnDestroy(){
        clearInterval(this.timer);
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