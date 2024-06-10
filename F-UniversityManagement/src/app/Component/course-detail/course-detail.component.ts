import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Course } from 'src/app/Models/course-model';
import { Professor } from 'src/app/Models/professor-model';
import { CourseService } from 'src/app/Services/Course/course.service';
import { ProfessorService } from 'src/app/Services/Professor/professor.service';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {

  id! : string
  course : Course = {} as Course
  prof : Professor = {} as Professor
  constructor(private route : ActivatedRoute, private courseService : CourseService , private profService : ProfessorService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(param => {
      this.id = param.get('id') as string
    });
    this.getCourse(this.id)
  }

  getCourse(courseId : string){
    this.courseService.getCourseById(courseId).subscribe({
      next : response => {
        this.course = response 
        this.getProf()
      },
      error : err =>{
        console.error('fetch course by id' , err)
      }
    })
  }

  getProf(){
      this.profService.getProf(this.course.professorId).subscribe({
        next : response =>{
          this.prof = response
        },error : err => {
          console.error('fetch prof' , err)
        }
      })
    }
}
