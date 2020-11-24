import { HttpClient } from '@angular/common/http';
import { UserStatistics } from './UserStatistics';
import {Injectable} from '@angular/core';

import { Observable, from } from 'rxjs';
import { map } from 'rxjs/operators'

@Injectable({providedIn: 'root'})
export class HTTPService{
  
    private data: UserStatistics[] = [];
    private link: string = 'https://localhost:5001/api/statistics/UserStatistics';

    constructor(private http: HttpClient){}
    
    getData(): UserStatistics[] {
    //getData(): Observable<any> {
        //return from(
        //    fetch(
        //      this.link, // the url you are trying to access
        //      {
        //        headers: {
        //          'Content-Type': 'application/json',
        //        },
        //        method: 'GET', // GET, POST, PUT, DELETE
        //        mode: 'no-cors' // the most important option
        //      }
        //    ));

        this.http.get(this.link).subscribe((data:UserStatistics[]) => this.data = data);

        return this.data;
    }
}