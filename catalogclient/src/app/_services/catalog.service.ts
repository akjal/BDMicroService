import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { University } from '../models/university.model';
import { Course } from '../models/course.model';

@Injectable({
  providedIn: 'root',
})
export class CatalogService {
  private http = inject(HttpClient);
  baseUrl = environment.apiConfig.catalogApiUri;

  getAllCourses() {
    return this.http.get(this.baseUrl + 'course');
  }
  addCourse(course: Course) {
    return this.http.post(this.baseUrl + 'course', course);
  }
  editCourse(id: number, course: Course) {
    return this.http.put(this.baseUrl + 'course/' + id, course);
  }
  deleteCourse(id: number) {
    return this.http.delete(this.baseUrl + 'course/' + id);
  }
  getCourseByUniversityId(universityId: string) {
    return this.http.get(this.baseUrl + 'course/university/' + universityId);
  }

  /*Univerisity*/
  getAllUniversities() {
    return this.http.get(this.baseUrl + 'university');
  }
  addUniversity(uni: University) {
    return this.http.post(this.baseUrl + 'university', uni);
  }
  editUniversity(id: number, uni: University) {
    return this.http.put(this.baseUrl + 'university/' + id, uni);
  }

  uploadPassport(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<any>(
      this.baseUrl + 'applications/passport/extract',
      formData
    );
  }
  submitApplication(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'applications', data);
  }
}
