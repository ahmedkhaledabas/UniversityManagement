import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CoursesComponent } from './courses/courses.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProfileComponent } from './profile/profile.component';
import { CollegeComponent } from './college/college.component';
import { HomeAdminComponent } from './home-admin/home-admin.component';
import { DepartmentComponent } from './Component/Admin/department/department.component';
import { CollegComponent } from './Component/Admin/colleg/colleg.component';
import { StudentComponent } from './Component/Admin/student/student.component';
import { ProfessorComponent } from './Component/Admin/professor/professor.component';
import { CourseComponent } from './Component/Admin/course/course.component';
import { EmployeeComponent } from './Component/Admin/employee/employee.component';
import { MyCoursesComponent } from './Component/my-courses/my-courses.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  {path : 'home' , component : HomeComponent},
  {path : 'courses' , component : CoursesComponent},
  {path : 'login' , component : LoginComponent},
  {path : 'dashboard' , component : DashboardComponent},
  {path : 'profile' , component : ProfileComponent},
  {path : 'college' , component : CollegeComponent},
  {path : 'admin/home' , component : HomeAdminComponent},
  {path : 'admin/college' , component : CollegComponent},
  {path : 'admin/department' , component : DepartmentComponent},
  {path : 'admin/student' , component : StudentComponent},
  {path : 'admin/professor' , component : ProfessorComponent},
  {path : 'admin/course' , component : CourseComponent},
  {path : 'admin/employee' , component : EmployeeComponent},
  {path : 'myCourses' , component : MyCoursesComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
