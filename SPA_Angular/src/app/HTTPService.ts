import { HttpClient } from '@angular/common/http';
import { UserStatistics } from './UserStatistics';
import { Events } from './Events';
import { Injectable } from '@angular/core';

import { Observable, from } from 'rxjs';
import { map } from 'rxjs/operators'

@Injectable({providedIn: 'root'})
export class HTTPService{
  
    private data: UserStatistics[] = [];
    private link: string = 'https://localhost:5001/api';

    constructor(private http: HttpClient){}
    
    getUsersList() {
        return this.http.get(this.link + '/statistics/UserStatistics');
    }

    getEventsForUser(ID: string) {
        return this.http.get(this.link + '/events/Events/descriptions/' + ID);
    }

    deleteEvents(ID: string) {
        this.http.delete(this.link + '/events/Events?ID=' + ID).subscribe();
    }

    createEventsDescription(user: UserStatistics) {
        let events: Events[] = [];

        for (var i = 0; i < user.events.length; i++){
            let event: Events = new Events();

            event.id = user.id;
            event.description = user.events[i].description;
            event.name = user.events[i].name;
            event.date = user.events[i].date;

            console.log(event.description);

            events.push(event);
        }

        this.http.put(this.link + '/events/Events', events).subscribe();
    }
}