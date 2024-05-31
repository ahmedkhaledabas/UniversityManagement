import { Component, OnInit } from '@angular/core';
import { CourseService } from '../Services/Course/course.service';
import { Course } from '../Models/course-model';
import { ProfessorService } from '../Services/Professor/professor.service';
import { Professor } from '../Models/professor-model';
import { CollegeService } from '../Services/College/college.service';
import { College } from '../Models/college-model';
import { DepartmentService } from '../Services/Department/department.service';

@Component({
  selector: 'courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {

  courses : Course[] =[]
  profs : Professor[] = []
  colleges : College[] = []
  role = sessionStorage.getItem('role')
  userName = sessionStorage.getItem('userName') as string

  constructor(private collegeService : CollegeService,private deptsService : DepartmentService, public courseService : CourseService, private profService : ProfessorService) {}
  ngOnInit(): void {
    this.getCourses()
    this.getProfessors()
    this.deptsService.getDepartments()
  }



filterDepts(deptId : string){
  return this.deptsService.departments.filter(d=>d.id == deptId)
}

  getCourses(){
    if(this.role != null){
      this.courseService.getCoursesForUser(this.userName).subscribe(
        (listCourses : Course[]) =>
          this.courses = listCourses
      )
    }else{
       this.courseService.getCourses().subscribe(
      (listCourses : Course[]) =>
        this.courses = listCourses
    )
    }
   
  }

  getProfessors(){
    this.profService.getProfessors().subscribe(
      (listProf : Professor[]) =>
        this.profs = listProf
    )
  }


}
