import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { Course } from '../courses/course-list/course-list.component';
import { University } from '../universities/uni-list/uni-list.component';

@Injectable({
  providedIn: 'root',
})
export class CatalogService {
  private http = inject(HttpClient);
  baseUrl = environment.apiConfig.catalogApiUri;

  getAllCourses() {
    return this.http.get(this.baseUrl + 'courses');
  }
  addCourse(course: Course) {
    return this.http.post(this.baseUrl + 'courses', course);
  }
  editCourse(id: number, course: Course) {
    return this.http.put(this.baseUrl + 'courses/' + id, course);
  }

  /*Univerisity*/
  getAllUniversities() {
    return this.http.get(this.baseUrl + 'universities');
  }
  addUniversity(uni: University) {
    return this.http.post(this.baseUrl + 'universities', uni);
  }
  editUniversity(id: number, uni: University) {
    return this.http.put(this.baseUrl + 'universities/' + id, uni);
  }
}
