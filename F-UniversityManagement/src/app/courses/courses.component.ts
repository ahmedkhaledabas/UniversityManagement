import { Component, OnInit } from '@angular/core';
import { CourseService } from '../Services/Course/course.service';
import { Course } from '../Models/course-model';
import { ProfessorService } from '../Services/Professor/professor.service';
import { Professor } from '../Models/professor-model';

@Component({
  selector: 'courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {

  courses : Course[] =[]
  profs : Professor[] = []

  constructor(public courseService : CourseService, private profService : ProfessorService) {}
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

  enroll(){
    console.log('aaaaaaa')
  }
}
