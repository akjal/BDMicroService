import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialogModule,
} from '@angular/material/dialog';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Component, inject, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';
import { University } from '../../universities/uni-list/uni-list.component';
import { CatalogService } from '../../_services/catalog.service';
import { MatOption, MatSelect } from '@angular/material/select';
@Component({
  selector: 'app-uni-edit',
  standalone: true,
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
  ],
  templateUrl: './uni-edit.component.html',
  styleUrl: './uni-edit.component.css',
})
export class UniEditComponent {
  http = inject(HttpClient);

  constructor(
    public dialogRef: MatDialogRef<UniEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: University
  ) {}

  onCancel(): void {
    this.dialogRef.close();
  }
}
