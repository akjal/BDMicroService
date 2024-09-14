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
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { ChangeDetectorRef } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { CourseEditComponent } from '../course-edit/course-edit.component';
import { CatalogService } from '../../_services/catalog.service';
@Component({
  selector: 'course-list',
  standalone: true,
  imports: [
    MatTableModule,
    MatIconModule,
    MatSortModule,
    MatPaginatorModule,
    MatButtonModule,
  ],
  templateUrl: './course-list.component.html',
  styleUrl: './course-list.component.css',
})
export class CourseListComponent implements AfterViewInit {
  http = inject(HttpClient);
  dialog = inject(MatDialog);
  catalogService = inject(CatalogService);
  cdr = inject(ChangeDetectorRef);
  title = 'Courses';
  courses!: Course[];
  dataSource!: MatTableDataSource<Course>;

  displayedColumns: string[] = ['name', 'duration', 'type', 'actions'];
  @ViewChild(MatSort) sort: MatSort = <MatSort>{};
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  private GetCourses() {
    this.catalogService.getAllCourses().subscribe({
      next: (response) => {
        this.courses = response as Course[];
        this.dataSource = new MatTableDataSource<Course>(this.courses);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }

  ngAfterViewInit() {
    this.GetCourses();
    console.log('paginator');
    console.log(this.paginator);

    this.cdr.detectChanges();
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
        this.catalogService.editCourse(result.id, result).subscribe({
          next: (response) => {
            const index = this.dataSource.data.findIndex(
              (u) => u.id === course.id
            );
            if (index !== -1) {
              this.dataSource.data[index] = result;
              this.dataSource = new MatTableDataSource(this.dataSource.data);
            }
          },
          error: (error) => console.log(error),
          complete: () => console.log('Edited a course'),
        });
      }
    });
  }

  addCourse(): void {
    let course = {} as Course;
    const dialogRef = this.dialog.open(CourseEditComponent, {
      width: '500px',
      data: course,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.catalogService.addCourse(result).subscribe({
          next: (response) => {
            this.courses = response as Course[];
            this.dataSource.data.push(result);
            this.dataSource.data = [...this.dataSource.data];
          },
          error: (error) => console.log(error),
          complete: () => console.log('Added a new course'),
        });
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
