import { Routes } from '@angular/router';
import { FailedComponent } from './failed/failed.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { MsalGuard } from '@azure/msal-angular';
import { CourseListComponent } from './courses/course-list/course-list.component';
import { UniListComponent } from './universities/uni-list/uni-list.component';
import { ApplicationAddComponent } from './applications/app-add/application-add/application-add.component';

export const routes: Routes = [
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [MsalGuard],
  },
  {
    path: 'course-list',
    component: CourseListComponent,
    canActivate: [MsalGuard],
  },
  {
    path: 'uni-list',
    component: UniListComponent,
    canActivate: [MsalGuard],
  },
  {
    path: 'application-add',
    component: ApplicationAddComponent,
    canActivate: [MsalGuard],
  },
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'login-failed',
    component: FailedComponent,
  },
];
