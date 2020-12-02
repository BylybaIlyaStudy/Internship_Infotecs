import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { AppComponent }   from './app.component';

import { UsersComponent }   from './Users/Users.component';
import { UsersWithEventsComponent }   from './UsersWithEvents/UsersWithEvents.component';

import { HttpClientModule }   from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

import { Routes, RouterModule } from '@angular/router';

const appRoutes: Routes =[
    { path: '', component: AppComponent },
    { path: 'users', component: UsersComponent },
    { path: 'users/:id', component: UsersWithEventsComponent }
];

@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule, 
                    BrowserAnimationsModule, MatTableModule, MatButtonModule, 
                    MatInputModule, RouterModule.forRoot(appRoutes) ],
    declarations: [ AppComponent, UsersComponent, UsersWithEventsComponent ],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }