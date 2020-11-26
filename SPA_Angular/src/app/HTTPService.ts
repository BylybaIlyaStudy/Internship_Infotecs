import { HttpClient } from '@angular/common/http';
import { UserStatistics } from './UserStatistics';
import {Injectable} from '@angular/core';

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
        return this.http.get(this.link + '/events/Events/' + ID);
    }
}