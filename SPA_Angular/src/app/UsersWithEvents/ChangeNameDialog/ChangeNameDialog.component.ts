import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { HTTPService } from '../../HTTPService';

@Component({
    selector: 'ChangeNameDialogComponent',
    templateUrl: './ChangeNameDialog.component.html'
  })

  export class ChangeNameDialogComponent {
    constructor(@Inject(MAT_DIALOG_DATA) public data: any, public dialogRef: MatDialogRef<ChangeNameDialogComponent>, private http: HTTPService) {}
    
    close() {
        this.dialogRef.close();
    }

    save() {
        this.dialogRef.close(this.data.user);
        this.http.changeUserName(this.data.user);
    }
}

