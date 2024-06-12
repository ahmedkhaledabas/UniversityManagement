
import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/Models/course-model';
import { Professor } from 'src/app/Models/professor-model';
import { CourseService } from 'src/app/Services/Course/course.service';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { ProfessorService } from 'src/app/Services/Professor/professor.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-my-courses',
  templateUrl: './my-courses.component.html',
  styleUrls: ['./my-courses.component.css']
})
export class MyCoursesComponent implements OnInit {
  courses : Course[] =[]
  profs : Professor[] = []
  user = sessionStorage.getItem('userName')
  userName : any

  constructor(private deptService : DepartmentService,public courseService : CourseService, private profService : ProfessorService , private userService : UserService) {}
   ngOnInit() {
    this.getMyCourses()
    this.getProfessors()
    this.deptService.getDepartments()
  }

  name(){
    console.log("dept")
  }

  filterDepts(deptId : string){
    return this.deptService.departments.filter(d=>d.id == deptId)
  }

  enroll(id : string){
    console.log('aaaaaaa')
  }
  
  getMyCourses(){
    const nameUser = sessionStorage.getItem('userName') as string

    this.courseService.getMyCourse(nameUser).subscribe(
      (listMyCourses : Course[]) =>
        this.courses = listMyCourses
    )
  }

  getProfessors(){
    this.profService.getProfessors().subscribe(
      (listProf : Professor[]) =>
        this.profs = listProf
    )
  }
}