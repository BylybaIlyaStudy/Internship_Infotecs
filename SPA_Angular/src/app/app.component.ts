import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { UserStatistics } from './UserStatistics';

@Component({
    selector: 'my-app',
    template:   `
                <div *ngIf="this.users.length > 0">
                    <div *ngIf="displayedEvents">
                        <div [ngStyle]="{
                                'width':'40%',
                                'display':'inline-block',
                                'margin-left':'20px'
                            }">
                            <table mat-table [dataSource]="users"  width ="100%">
                                <ng-container matColumnDef="name">
                                    <th mat-header-cell *matHeaderCellDef> Имя узла </th>
                                    <td mat-cell *matCellDef="let item" (click) = "setEvents(item)"> {{item.name}} </td>
                                </ng-container>
                                <ng-container matColumnDef="date">
                                    <th mat-header-cell *matHeaderCellDef> Дата последней статистики </th>
                                    <td mat-cell *matCellDef="let item" (click) = "setEvents(item)"> {{item.date}} </td>
                                </ng-container>
                                <tr mat-header-row *matHeaderRowDef="displayedColumnsWithEvents"></tr>
                                <tr mat-row *matRowDef="let row; columns: displayedColumnsWithEvents;"></tr>
                            </table>
                        </div>
                        <div [ngStyle]="{
                                'width':'50%',
                                'float':'right',
                                'margin-top':'20px',
                                'margin-left':'20px'
                            }" (click) = "setEvents(userWhoseEventsAreSelected)">
                            <p>Идентификатор: {{userWhoseEventsAreSelected.id}}</p>
                            <p>Версия ПО ViPNet Client: {{userWhoseEventsAreSelected.version}}</p>
                            <b>События ViPNet Client</b>

                            <table mat-table [dataSource]="userWhoseEventsAreSelected.events"  width ="100%">
                                <ng-container matColumnDef="name">
                                    <th mat-header-cell *matHeaderCellDef> Событие </th>
                                    <td mat-cell *matCellDef="let item"> {{item.name}} </td>
                                </ng-container>
                                <ng-container matColumnDef="date">
                                    <th mat-header-cell *matHeaderCellDef> Дата </th>
                                    <td mat-cell *matCellDef="let item"> {{item.date}} </td>
                                </ng-container>
                                <tr mat-header-row *matHeaderRowDef="displayedColumnsEvents"></tr>
                                <tr mat-row *matRowDef="let row; columns: displayedColumnsEvents;"></tr>
                            </table>
                        </div>
                    </div>
                    <div *ngIf="!displayedEvents">
                        <table mat-table [dataSource]="users" width ="100%">
                            <ng-container matColumnDef="name">
                                <th mat-header-cell *matHeaderCellDef> Имя узла </th>
                                <td mat-cell *matCellDef="let item" (click) = "setEvents(item)"> {{item.name}} </td>
                            </ng-container>
                            <ng-container matColumnDef="date">
                                <th mat-header-cell *matHeaderCellDef> Дата последней статистики </th>
                                <td mat-cell *matCellDef="let item" (click) = "setEvents(item)"> {{item.date}} </td>
                            </ng-container>
                            <ng-container matColumnDef="version">
                                <th mat-header-cell *matHeaderCellDef> Версия Client </th>
                                <td mat-cell *matCellDef="let item" (click) = "setEvents(item)"> {{item.version}} </td>
                            </ng-container>
                            <ng-container matColumnDef="os">
                                <th mat-header-cell *matHeaderCellDef> Тип устройства </th>
                                <td mat-cell *matCellDef="let item" (click) = "setEvents(item)"> {{item.os}} </td>
                            </ng-container>
                            <tr mat-header-row *matHeaderRowDef="displayedColumnsWithoutEvents"></tr>
                            <tr mat-row *matRowDef="let row; columns: displayedColumnsWithoutEvents;"></tr>
                        </table>
                    </div>
                </div>
                <p *ngIf="this.users.length == 0">
                    Загрузка...
                </p>
                `
})

export class AppComponent { 
    users: UserStatistics[] = [];
    userWhoseEventsAreSelected: UserStatistics;

    displayedColumnsWithoutEvents: string[] = ['name', 'date', 'version', 'os'];
    displayedColumnsWithEvents: string[] = ['name', 'date'];
    displayedColumnsEvents: string[] = ['name', 'date'];

    displayedEvents: boolean = false;

    constructor(private http: HttpClient){}
    
    timeLeft: number = 0;
    time: number = 30;
    timer;

    ngOnInit(){
        this.getData();
        this.timer = setInterval(() => {
            if(this.timeLeft < this.time) {
                this.timeLeft++;
                console.log(this.timeLeft);
            } else {
                this.getData();
                this.timeLeft = 10;
            }
        }, 1000);
    }

    ngOnDestroy(){
        clearInterval(this.timer);
    }

    setEvents(user: UserStatistics) {
        if (user == this.userWhoseEventsAreSelected){
            this.displayedEvents = !this.displayedEvents;
        }
        else {
            this.userWhoseEventsAreSelected = user;
            this.displayedEvents = true;
        }
    }

    getData(){
        this.http.get('https://localhost:5001/api/statistics/UserStatistics').subscribe((data:UserStatistics[]) => this.users=data);
    }
}