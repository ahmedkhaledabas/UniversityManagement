import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HeroComponent } from './hero/hero.component';
import { HomeComponent } from './home/home.component';
import { CourseCardComponent } from './course-card/course-card.component';
import { CoursesComponent } from './courses/courses.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProfileComponent } from './profile/profile.component';
import { CollegeComponent } from './college/college.component';
import { CollegeAdminComponent } from './college-admin/college-admin.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { HomeAdminComponent } from './home-admin/home-admin.component';
import {HttpClientModule} from '@angular/common/http'
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { CollegComponent } from './Component/Admin/colleg/colleg.component';
import { CourseComponent } from './Component/Admin/course/course.component';
import { DepartmentComponent } from './Component/Admin/department/department.component';
import { PaginationComponent } from './Component/Shared/pagination/pagination.component';
import { StudentComponent } from './Component/Admin/student/student.component';
import { ProfessorComponent } from './Component/Admin/professor/professor.component';
import { EmployeeComponent } from './Component/Admin/employee/employee.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HeroComponent,
    HomeComponent,
    CourseCardComponent,
    CoursesComponent,
    FooterComponent,
    LoginComponent,
    DashboardComponent,
    ProfileComponent,
    CollegeComponent,
    CollegeAdminComponent,
    SidebarComponent,
    HomeAdminComponent,
    CollegComponent,
    CourseComponent,
    DepartmentComponent,
    PaginationComponent,
    StudentComponent,
    ProfessorComponent,
    EmployeeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
