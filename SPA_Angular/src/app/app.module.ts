import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { AppComponent }   from './app.component';

import { UsersComponent }   from './Users.component';
import { EventsComponent }   from './Events.component';
import { UsersWithEventsComponent }   from './UsersWithEvents.component';

import { HttpClientModule }   from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule, BrowserAnimationsModule, MatTableModule, MatButtonModule, MatInputModule ],
    declarations: [ AppComponent, UsersComponent, EventsComponent, UsersWithEventsComponent ],
    bootstrap:    [ AppComponent ],
    exports:[ MatTableModule ]
})
export class AppModule { }