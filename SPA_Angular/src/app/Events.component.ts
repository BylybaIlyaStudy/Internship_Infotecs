import { Input, Output, Component, EventEmitter } from '@angular/core';
import { UserStatistics } from './UserStatistics';

@Component({
    selector: 'Events-component',
    templateUrl: './Events.component.html',
    styleUrls: ['./Events.component.css']
})

export class EventsComponent { 
    @Input() displayedColumns: string[];

    @Input() user: UserStatistics;
    @Output() onSetUser = new EventEmitter<UserStatistics>();
    @Output() onDeleteEvents = new EventEmitter<UserStatistics>();
    @Output() onCreateDescription = new EventEmitter<UserStatistics>()

    enteringDescription: boolean = false;

    setUser() {
        this.onSetUser.emit(this.user);
    }

    createDescription() {
        this.changeEnteringDescription();

        this.onCreateDescription.emit(this.user);
    }

    changeEnteringDescription() {
        this.enteringDescription = !this.enteringDescription;
    }

    deleteEvents() {
        this.onDeleteEvents.emit(this.user);
    }
}