import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialogModule,
} from '@angular/material/dialog';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Component, Inject, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Course } from '../course-list/course-list.component';
import { MatButtonModule } from '@angular/material/button';
import { CatalogService } from '../../_services/catalog.service';

@Component({
  selector: 'course-edit',
  templateUrl: './course-add.component.html',
  standalone: true,
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
  ],
})
export class CourseAddComponent {
  constructor(
    public dialogRef: MatDialogRef<CourseAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Course
  ) {}

  onCancel(): void {
    this.dialogRef.close();
  }
}
