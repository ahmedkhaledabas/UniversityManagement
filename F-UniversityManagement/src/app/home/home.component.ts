import { Component, OnInit } from '@angular/core';
import { CourseService } from '../Services/Course/course.service';
import { Course } from '../Models/course-model';
import { Professor } from '../Models/professor-model';
import { ProfessorService } from '../Services/Professor/professor.service';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  role = sessionStorage.getItem('role')
  courses : Course[] =[]
  profs : Professor[] =[]
  constructor(public courseService : CourseService , private profService : ProfessorService ) {}
  ngOnInit(): void {
    this.getCourses()
    this.getProfessors()
    
  }

  getCourses(){
      this.courseService.getCourses().subscribe(
      (listCourses : Course[]) =>
        this.courses = listCourses
    )
  }

  getProfessors(){
    this.profService.getProfessors().subscribe(
      (listProf : Professor[]) =>
        this.profs = listProf
    )
  }
}
