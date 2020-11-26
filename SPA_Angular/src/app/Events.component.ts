import { Input, Output, Component, EventEmitter } from '@angular/core';
import { UserStatistics } from './UserStatistics';
      
@Component({
    selector: 'Events-component',
    templateUrl: './Events.component.html'
})

export class EventsComponent { 
    @Input() displayedColumnsEvents: string[] = ['name', 'date'];

    @Input() user: UserStatistics;
    @Output() onChanged = new EventEmitter<UserStatistics>();

    setUser(user: UserStatistics) {
        this.onChanged.emit(user);
    }
}