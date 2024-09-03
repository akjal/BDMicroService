import {
  Component,
  OnInit,
  inject,
  AfterViewInit,
  ViewChild,
} from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatSort } from '@angular/material/sort';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

import { HttpClient } from '@angular/common/http';
import { CourseEditComponent } from '../course-edit/course-edit.component';
import { CatalogService } from '../../_services/catalog.service';
@Component({
  selector: 'course-list',
  standalone: true,
  imports: [
    MatTableModule,
    MatIconModule,
    MatSort,
    MatPaginator,
    MatButtonModule,
  ],
  templateUrl: './course-list.component.html',
  styleUrl: './course-list.component.css',
})
export class CourseListComponent implements OnInit {
  http = inject(HttpClient);
  dialog = inject(MatDialog);
  catalogService = inject(CatalogService);

  title = 'Courses';
  courses: Course[] = [];
  dataSource = new MatTableDataSource(this.courses);

  displayedColumns: string[] = ['name', 'duration', 'type', 'actions'];

  ngOnInit(): void {
    this.catalogService.getAllCourses().subscribe({
      next: (response) => {
        this.courses = response as Course[];
        this.dataSource = new MatTableDataSource<Course>(this.courses);
      },
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }
  delete(arg0: any) {
    console.log(arg0);
  }
  editCourse(course: Course): void {
    const dialogRef = this.dialog.open(CourseEditComponent, {
      width: '500px',
      data: { ...course },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const index = this.dataSource.data.findIndex((u) => u.id === course.id);
        if (index !== -1) {
          this.dataSource.data[index] = result;
          this.dataSource = new MatTableDataSource(this.dataSource.data);
        }
      }
    });
  }
}

export interface Course {
  name: string;
  id: number;
  duration: number;
  type: string;
}
