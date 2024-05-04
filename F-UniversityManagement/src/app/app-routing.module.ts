import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CoursesComponent } from './courses/courses.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProfileComponent } from './profile/profile.component';
import { CollegeComponent } from './college/college.component';
import { HomeAdminComponent } from './home-admin/home-admin.component';
import { CollegeAdminComponent } from './college-admin/college-admin.component';

const routes: Routes = [
  {path : 'home' , component : HomeComponent},
  {path : 'courses' , component : CoursesComponent},
  {path : 'login' , component : LoginComponent},
  {path : 'dashboard' , component : DashboardComponent},
  {path : 'profile' , component : ProfileComponent},
  {path : 'college' , component : CollegeComponent},
  {path : 'admin/home' , component : HomeAdminComponent},
  {path : 'admin/college' , component : CollegeAdminComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
