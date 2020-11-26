import { Input, Output, Component, EventEmitter } from '@angular/core';
import { UserStatistics } from './UserStatistics';

@Component({
    selector: 'UsersWithEvents-component',
    templateUrl: './UsersWithEvents.component.html'
})

export class UsersWithEventsComponent { 
    @Input() displayedColumnsWithEvents: string[] = ['name', 'date'];

    @Input() users: UserStatistics[];
    @Output() onChanged = new EventEmitter<UserStatistics>();

    setUser(user: UserStatistics) {
        this.onChanged.emit(user);
    }
}