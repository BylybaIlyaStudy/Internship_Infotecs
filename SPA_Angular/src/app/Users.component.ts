import { Input, Output, Component, EventEmitter } from '@angular/core';
import { UserStatistics } from './UserStatistics';

@Component({
    selector: 'Users-component',
    templateUrl: './Users.component.html'
})

export class UsersComponent { 
    @Input() displayedColumnsWithoutEvents: string[] = ['name', 'date', 'version', 'os'];

    @Input() users: UserStatistics[];
    @Output() onChanged = new EventEmitter<UserStatistics>();

    setUser(user: UserStatistics) {
        this.onChanged.emit(user);
    }
}