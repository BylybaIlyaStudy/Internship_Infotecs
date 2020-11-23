import { HttpClient } from '@angular/common/http';
import { UserStatistics } from './UserStatistics';
import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class HTTPService{
  
    private data: UserStatistics[] = [];
    private link: string = 'https://localhost:5001/api/statistics/UserStatistics';

    constructor(private http: HttpClient){}
    
    getData(): UserStatistics[] {
        this.http.get(this.link).subscribe((data:UserStatistics[]) => this.data = data);

        return this.data;
    }
}