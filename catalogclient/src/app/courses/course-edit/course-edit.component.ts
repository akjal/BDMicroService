import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialogModule,
} from '@angular/material/dialog';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Course } from '../course-list/course-list.component';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'course-edit',
  templateUrl: './course-edit.component.html',
  standalone: true,
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
  ],
})
export class CourseEditComponent {
  constructor(
    public dialogRef: MatDialogRef<CourseEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Course
  ) {}

  onCancel(): void {
    this.dialogRef.close();
  }
}
