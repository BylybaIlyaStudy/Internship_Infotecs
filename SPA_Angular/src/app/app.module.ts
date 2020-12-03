import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';

import { UsersComponent } from './Users/Users.component';
import { UsersWithEventsComponent } from './UsersWithEvents/UsersWithEvents.component';
import { ChangeNameDialogComponent } from './UsersWithEvents/ChangeNameDialog/ChangeNameDialog.component';

import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatSelectModule } from '@angular/material/select';
import { Routes, RouterModule } from '@angular/router';

const appRoutes: Routes =[
    { path: '', component: AppComponent },
    { path: 'users', component: UsersComponent },
    { path: 'users/:id', component: UsersWithEventsComponent }
];

@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule, 
                    BrowserAnimationsModule, MatTableModule, MatButtonModule, 
                    MatInputModule, RouterModule.forRoot(appRoutes), MatDialogModule, 
                    MatDividerModule, MatSelectModule ],
    declarations: [ AppComponent, UsersComponent, UsersWithEventsComponent, ChangeNameDialogComponent ],
    bootstrap:    [ AppComponent ],
    entryComponents: [ ChangeNameDialogComponent ]
})
export class AppModule { }