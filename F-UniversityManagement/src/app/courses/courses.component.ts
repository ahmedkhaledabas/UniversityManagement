import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { CourseService } from '../Services/Course/course.service';
import { Course } from '../Models/course-model';
import { ProfessorService } from '../Services/Professor/professor.service';
import { Professor } from '../Models/professor-model';
import { CollegeService } from '../Services/College/college.service';
import { College } from '../Models/college-model';
import { DepartmentService } from '../Services/Department/department.service';
import { StudentService } from '../Services/Student/student.service';
import { Toast, ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: 'courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {

  courses : Course[] =[]
  profs : Professor[] = []
  colleges : College[] = []
  profId! : string
  role = sessionStorage.getItem('role')
  userName = sessionStorage.getItem('userName') as string

  constructor(private router : Router,private route : ActivatedRoute , private toastr : ToastrService ,private studentService : StudentService , private elementRef : ElementRef ,private collegeService : CollegeService,private deptsService : DepartmentService, public courseService : CourseService, private profService : ProfessorService) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe(param => {
      this.profId  = param.get('profId') as string
    })
    this.getCourses()
    this.getProfessors()
    this.deptsService.getDepartments()
  }


enroll(courseId : string){
  if(this.userName == null){
    this.router.navigate(['login'])
  }else{
  this.studentService.enrollCourse(courseId , this.userName).subscribe({
    next : response =>{
      this.toastr.success("Course Add To You" , "Success")
      this.getCourses()
    },
    error : err =>{
      this.toastr.error("Course Add To You" , "Failed")
    }
  })
}
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
