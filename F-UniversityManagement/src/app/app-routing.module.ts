import { ProfessorsComponent } from './Component/professors/professors.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CoursesComponent } from './courses/courses.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProfileComponent } from './profile/profile.component';
import { CollegeComponent } from './college/college.component';
import { DepartmentComponent } from './Component/Admin/department/department.component';
import { CollegComponent } from './Component/Admin/colleg/colleg.component';
import { StudentComponent } from './Component/Admin/student/student.component';
import { ProfessorComponent } from './Component/Admin/professor/professor.component';
import { CourseComponent } from './Component/Admin/course/course.component';
import { EmployeeComponent } from './Component/Admin/employee/employee.component';
import { MyCoursesComponent } from './Component/my-courses/my-courses.component';
import { QuizComponent } from './Component/quiz/quiz.component';
import { ResultComponent } from './Component/result/result.component';
import { CourseDetailComponent } from './Component/course-detail/course-detail.component';
import { ProfessorCoursesComponent } from './Component/professor-courses/professor-courses.component';
import { FeeComponent } from './Component/fee/fee.component';
import { AuthGurd } from './Services/auth-gurd.service';
import { ErrorNotAccessComponent } from './Component/error-not-access/error-not-access.component';
import { AuthGurdAdmin } from './Services/auth-gurd-admin.service';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  {path : 'home' , component : HomeComponent},
  {path : 'courses' , component : CoursesComponent},
  {path : 'professor-courses/:profId' , component : ProfessorCoursesComponent , canActivate : [AuthGurd]},
  {path : 'login' , component : LoginComponent},
  {path : 'dashboard' , component : DashboardComponent, canActivate : [AuthGurd] },
  {path : 'profile' , component : ProfileComponent , canActivate : [AuthGurd]} ,
  {path : 'college' , component : CollegeComponent},
  {path : 'admin/college' , component : CollegComponent, canActivate : [AuthGurd, AuthGurdAdmin ]},
  {path : 'admin/department' , component : DepartmentComponent, canActivate : [AuthGurd , AuthGurdAdmin]},
  {path : 'admin/student' , component : StudentComponent , canActivate : [AuthGurd , AuthGurdAdmin]},
  {path : 'admin/professor' , component : ProfessorComponent , canActivate : [AuthGurd , AuthGurdAdmin]},
  {path : 'admin/course' , component : CourseComponent , canActivate : [AuthGurd , AuthGurdAdmin]},
  {path : 'admin/employee' , component : EmployeeComponent, canActivate : [AuthGurd , AuthGurdAdmin]},
  {path : 'myCourses' , component : MyCoursesComponent, canActivate : [AuthGurd]},
  {path : 'quiz' , component : QuizComponent, canActivate : [AuthGurd]},
  {path : 'result' , component : ResultComponent},
  {path : 'course-detail/:id' , component : CourseDetailComponent},
  {path : 'professors' , component : ProfessorsComponent},
  {path : 'fee' , component : FeeComponent},
  {path : 'notaccess' , component : ErrorNotAccessComponent ,  canActivate : [AuthGurd]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
