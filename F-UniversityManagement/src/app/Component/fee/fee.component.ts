import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/Models/course-model';
import { CourseService } from 'src/app/Services/Course/course.service';

@Component({
  selector: 'app-fee',
  templateUrl: './fee.component.html',
  styleUrls: ['./fee.component.css']
})
export class FeeComponent implements OnInit{
  
  courses : Course[] = []

  constructor(private courseService : CourseService) {}
  
  ngOnInit(): void {
    this.getMyCourses()
  }

  getMyCourses(){
    const nameUser = sessionStorage.getItem('userName') as string
    this.courseService.getMyCourse(nameUser).subscribe(
      (listMyCourses : Course[]) =>
        this.courses = listMyCourses
    )
  }

}
