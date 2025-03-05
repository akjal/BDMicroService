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
import { CatalogService } from '../../_services/catalog.service';
import { UniEditComponent } from '../uni-edit/uni-edit.component';

@Component({
  selector: 'app-uni-list',
  standalone: true,
  imports: [
    MatTableModule,
    MatIconModule,
    MatSortModule,
    MatPaginatorModule,
    MatButtonModule,
  ],
  templateUrl: './uni-list.component.html',
  styleUrl: './uni-list.component.css',
})
export class UniListComponent implements AfterViewInit {
  http = inject(HttpClient);
  dialog = inject(MatDialog);
  catalogService = inject(CatalogService);
  cdr = inject(ChangeDetectorRef);
  title = 'Universities';
  universities!: University[];
  dataSource!: MatTableDataSource<University>;
  displayedColumns: string[] = ['name', 'webPage', 'country', 'state'];
  @ViewChild(MatSort) sort: MatSort = <MatSort>{};
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  ngAfterViewInit() {
    this.GetUniversities();
    console.log('paginator');
    console.log(this.paginator);

    this.cdr.detectChanges();
  }
  private GetUniversities() {
    this.catalogService.getAllUniversities().subscribe({
      next: (response) => {
        this.universities = response as University[];
        this.dataSource = new MatTableDataSource<University>(this.universities);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }
  adduniversity(): void {
    let uni = {} as University;
    const dialogRef = this.dialog.open(UniEditComponent, {
      width: '800px',
      data: uni,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.catalogService.addUniversity(result).subscribe({
          next: (response) => {
            this.universities = response as University[];
            this.dataSource.data.push(result);
            this.dataSource.data = [...this.dataSource.data];
          },
          error: (error) => console.log(error),
          complete: () => console.log('Added a new university'),
        });
      }
    });
  }
}

export interface University {
  name: string;
  id: number;
  country: string;
  webPages: string;
  countryCode: string;
  state: string;
}
