import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Course } from 'src/app/Models/course-model';
import { Professor } from 'src/app/Models/professor-model';
import { CourseService } from 'src/app/Services/Course/course.service';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { ProfessorService } from 'src/app/Services/Professor/professor.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-professor-courses',
  templateUrl: './professor-courses.component.html',
  styleUrls: ['./professor-courses.component.css']
})
export class ProfessorCoursesComponent implements OnInit {
  profId! : string
  user : any
  courses : Course[] = []
  profs: Professor[] =[]
  
  constructor(private profService : ProfessorService,private route : ActivatedRoute , private coursesService : CourseService,private deptService : DepartmentService) {
 
  }

  ngOnInit(): void {
    this.deptService.getDepartments()
    this.getProfessors()
    this.route.paramMap.subscribe(param => {
      this.profId  = param.get('profId') as string
    })
    this.getCourses()
  }

  getCourses(){
    this.coursesService.getCourses().subscribe({
      next : response =>{
        this.courses = response
      }
    })
  }

  filterCourses(){
    return this.courses.filter(c=>c.professorId == this.profId)
  }

  
  filterDepts(deptId : string){
    return this.deptService.departments.filter(d=>d.id == deptId)
  }
  
  getProfessors(){
    this.profService.getProfessors().subscribe(
      (listProf : Professor[]) =>
        this.profs = listProf
    )
  }
}
