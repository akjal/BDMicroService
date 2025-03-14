import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {
  MatNativeDateModule,
  provideNativeDateAdapter,
} from '@angular/material/core';

import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { University } from '../../../universities/uni-list/uni-list.component';
import { CatalogService } from '../../../_services/catalog.service';
import { Course } from '../../../courses/course-list/course-list.component';
@Component({
  selector: 'application-add',
  standalone: true,
  imports: [
    CommonModule,
    MatStepperModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
  ],
  templateUrl: './application-add.component.html',
  styleUrl: './application-add.component.css',
})
export class ApplicationAddComponent {
  http = inject(HttpClient);
  universities!: University[];
  universityCourses!: Course[];
  catalogService = inject(CatalogService);
  profileFormGroup: FormGroup;
  universityFormGroup: FormGroup;
  courseFormGroup: FormGroup;

  constructor(private _formBuilder: FormBuilder) {
    this.GetUniversities();
    this.profileFormGroup = this._formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      passportNumber: ['', Validators.required],
      passportIssueDate: ['', Validators.required],
      passportExpiryDate: ['', Validators.required],
    });

    this.universityFormGroup = this._formBuilder.group({
      university: ['', Validators.required],
    });

    this.courseFormGroup = this._formBuilder.group({
      course: ['', Validators.required],
    });
  }

  submitApplication() {
    if (
      this.profileFormGroup.valid &&
      this.universityFormGroup.valid &&
      this.courseFormGroup.valid
    ) {
      const applicationData = {
        ...this.profileFormGroup.value,
        ...this.universityFormGroup.value,
        ...this.courseFormGroup.value,
      };

      console.log('Submitting data:', applicationData);

      this.catalogService.submitApplication(applicationData).subscribe({
        next: (res) => {
          console.log('Application submitted successfully:', res);
          alert('Application submitted successfully!');
        },
        error: (err) => {
          console.error('Failed to submit application:', err);
          alert('Submission failed. Please try again.');
        },
      });

      console.log('Application Submitted:', applicationData);
      alert('Application submitted successfully!');
    }
  }
  onStepChange(event: any) {
    if (event.selectedIndex === 2) {
      // Step 2
      this.GetUniversityCourses();
    }
  }

  private GetUniversityCourses() {
    var selectedUni = this.universityFormGroup.value.university;
    var selectedUniId =
      this.universities.find((x) => x.name === selectedUni)?.id ?? 0;

    this.catalogService.getCourseByUniversityId(selectedUniId).subscribe({
      next: (response) => {
        this.universityCourses = response as Course[];
      },
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
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
  onDragOver(event: DragEvent) {
    event.preventDefault();
    (event.currentTarget as HTMLElement).classList.add('dragover');
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    (event.currentTarget as HTMLElement).classList.remove('dragover');
  }

  onFileDropped(event: DragEvent) {
    event.preventDefault();
    const file = event.dataTransfer?.files[0];
    if (file) this.uploadPassportFile(file);
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) this.uploadPassportFile(file);
  }

  uploadPassportFile(file: File) {
    this.catalogService.uploadPassport(file).subscribe({
      next: (data) => {
        this.profileFormGroup.patchValue({
          fullName: data.fullName,
          email: data.email,
          passportNumber: data.passportNumber,
          passportIssueDate: new Date(data.passportIssueDate),
          passportExpiryDate: new Date(data.passportExpiryDate),
        });
      },
      error: (error) => {
        console.error('Passport upload failed:', error);
      },
    });
  }
}
