import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { HttpClient } from '@angular/common/http';
import { CatalogService } from '../../../_services/catalog.service';
import { University } from '../../../models/university.model';
import { Course } from '../../../models/course.model';

@Component({
  selector: 'student-app',
  standalone: true,
  templateUrl: './student-app.component.html',
  styleUrls: ['./student-app.component.scss'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCardModule,
    MatSnackBarModule,
  ],
})
export class StudentAppComponent {
  private fb = inject(FormBuilder);
  catalogService = inject(CatalogService);
  private snackBar = inject(MatSnackBar);

  studentForm: FormGroup;
  courseForm: FormGroup;
  documentForm: FormGroup;

  universities: University[] = [];
  allCourses: Course[] = [];
  filteredCourses: Course[] = [];
  uploadedFiles: { [key: string]: File } = {};

  constructor() {
    this.studentForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', Validators.required],
      nationality: ['', Validators.required],
      contactNumber: [''],
      address: [''],
    });

    this.courseForm = this.fb.group({
      universityId: ['', Validators.required],
      courseId: ['', Validators.required],
    });

    this.documentForm = this.fb.group({
      passport: [null],
      transcript: [null],
    });
  }

  ngOnInit() {
    this.catalogService.getAllUniversities().subscribe({
      next: (data) => (this.universities = data as University[]),
      error: () =>
        this.snackBar.open('Failed to load universities', 'Close', {
          duration: 3000,
        }),
    });

    this.catalogService.getAllCourses().subscribe({
      next: (data) => (this.allCourses = data as Course[]),
      error: () =>
        this.snackBar.open('Failed to load courses', 'Close', {
          duration: 3000,
        }),
    });
  }

  onUniversityChange() {
    const uniId = this.courseForm.value.universityId;
    this.filteredCourses = this.allCourses.filter(
      (c) => c.universityId === uniId
    );
  }

  onFileSelected(event: Event, type: string) {
    const input = event.target as HTMLInputElement;
    if (input?.files?.length) {
      this.uploadedFiles[type] = input.files[0];
    }
  }

  getUniversityName(id: string): string {
    return this.universities.find((u) => u.id === id)?.name || '';
  }

  getCourseName(id: string): string {
    return this.allCourses.find((c) => c.id === id)?.name || '';
  }

  submitApplication() {
    const applicationPayload = {
      student: this.studentForm.value,
      courseSelection: this.courseForm.value,
      documents: this.uploadedFiles,
    };

    console.log('Submitting application:', applicationPayload);
    this.snackBar.open('Application submitted successfully!', 'Close', {
      duration: 3000,
    });
  }
}
