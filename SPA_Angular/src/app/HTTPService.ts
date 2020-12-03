import { HttpClient } from '@angular/common/http';
import { UserStatistics } from './Models/UserStatistics';
import { Events } from './Models/Events';
import { Injectable } from '@angular/core';

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

    createEventsDescription(events: Events[]) {
        this.http.put(this.link + '/events/Events', events).subscribe();
    }
}