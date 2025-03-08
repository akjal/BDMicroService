import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialogModule,
} from '@angular/material/dialog';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Component, inject, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Course } from '../course-list/course-list.component';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient } from '@angular/common/http';
import { University } from '../../universities/uni-list/uni-list.component';
import { CatalogService } from '../../_services/catalog.service';
import { MatOption, MatSelect } from '@angular/material/select';

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
    MatSelect,
    MatOption,
  ],
})
export class CourseEditComponent {
  http = inject(HttpClient);
  universities!: University[];
  catalogService = inject(CatalogService);
  selectedUniversity!: string;
  constructor(
    public dialogRef: MatDialogRef<CourseEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Course
  ) {
    this.GetUniversities();
    this.selectedUniversity = data.university;
  }
  private GetUniversities() {
    this.catalogService.getAllUniversities().subscribe({
      next: (response) => {
        this.universities = response as University[];
      },
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }
  onCancel(): void {
    this.dialogRef.close();
  }
}
