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
import { MatOption, MatSelect } from '@angular/material/select';
import { HttpClient } from '@angular/common/http';
import { University } from '../../universities/uni-list/uni-list.component';

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
    MatSelect,
    MatOption,
  ],
})
export class CourseAddComponent {
  http = inject(HttpClient);
  universities!: University[];
  catalogService = inject(CatalogService);

  constructor(
    public dialogRef: MatDialogRef<CourseAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Course
  ) {}
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
