import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { College } from 'src/app/Models/college-model';
import { Course } from 'src/app/Models/course-model';
import { Quiz } from 'src/app/Models/quiz-model';
import { CollegeService } from 'src/app/Services/College/college.service';
import { CourseService } from 'src/app/Services/Course/course.service';
import { QuizService } from 'src/app/Services/Quiz/quiz.service';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit{

  role = sessionStorage.getItem('role')
  userName = sessionStorage.getItem('userName') as string
  courses : Course [] = []
  question : Quiz = {} as Quiz
  answer!:string
  degree : number = 0
  fullMark : number = 0
  message : string =''

  
  constructor(public quizService : QuizService, private router : Router , private courseService : CourseService , private toastr : ToastrService) {
    
  }
  ngOnInit(): void {
    this.getMyCourses()
    if(this.role == 'Student'){
      this.getQuestions(this.userName)
    }
    

  }

  submitAnswer(ques : Quiz , answer : string){

    if(answer == this.answer) this.degree++
    this.fullMark = this.quizService.qus.length

    
  }


  getMyCourses(){
    const nameUser = sessionStorage.getItem('userName') as string
    this.courseService.getMyCourse(nameUser).subscribe(
      (listMyCourses : Course[]) =>
        this.courses = listMyCourses
    )
  }

  getQuestions(userName : string){
    //this.quizService.qnProgress = 0
    //this.quizService.second = 0
    this.quizService.getQuestions(userName).subscribe(
      (data : any) =>{
        this.quizService.qus = data 
        this.startTimer()
      }
    )
  }

  startTimer(){
    this.quizService.timer = setInterval(()=> {
      this.quizService.second++;
    },1000)
  }

  endQuiz(){
    clearInterval(this.quizService.timer)
  }

  generateRandomString(length : number){
    const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'
    let random = ''
    const characterLength = chars.length
    for(let i = 0 ; i < length ; i++){
      random+= chars.charAt(Math.floor(Math.random() * characterLength))
    }
    return random
  }
  addQuestion(){
    this.question.professorId = sessionStorage.getItem('userName') as string
    this.question.id = this.generateRandomString(4)
    console.log(this.question)
    this.quizService.creatQuestion(this.question).subscribe({
      next : response => {
        this.toastr.success('Question Addes' , 'Success')
        this.question = {} as Quiz
      },
      error : err => {
        this.toastr.error('question Add' , 'Invalid')
      }
    })
  }

  openModal(){
    const modal = document.getElementById('exampleModal');
    if(modal != null){
      modal.style.display = 'block' ;
    }
    if(this.degree >= (this.fullMark / 2)){
      this.message = 'You Are Success'
    }else this.message = 'You Are Failed'
   }
  
   closeModal(){
    const modal = document.getElementById('exampleModal');
    if(modal != null){
      modal.style.display = 'none' ;
    }
    this.router.navigate(['home']).then(() => {
      window.location.reload();}
     )
   }
}
