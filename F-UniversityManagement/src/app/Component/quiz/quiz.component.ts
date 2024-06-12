import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { College } from 'src/app/Models/college-model';
import { Course } from 'src/app/Models/course-model';
import { Question } from 'src/app/Models/question-model';
import { Quiz } from 'src/app/Models/quiz-model';
import { CollegeService } from 'src/app/Services/College/college.service';
import { CourseService } from 'src/app/Services/Course/course.service';
import { QuizService } from 'src/app/Services/Quiz/quiz.service';
import { SharedService } from 'src/app/Services/Shared/shared.service';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit{

  role = sessionStorage.getItem('role')
  userName = sessionStorage.getItem('userName') as string
  courses : Course [] = []
  quiz : Quiz = {} as Quiz
  answer!:null
  degree : number = 0
  fullMark : number = 0
  message : string =''
  question : Question = {} as Question
  questions : Question[] = []
  finalQuestions : Question[] = []
  quizes : Quiz [] = []
  quizStarted : any = null
  i : number = 1
  
  constructor(public sharedService : SharedService ,public quizService : QuizService, private router : Router , private courseService : CourseService , private toastr : ToastrService) {
    
  }

  ngOnInit(): void {
    this.getMyCourses()
    if(this.role == 'Student'){
      this.getQuiz(this.userName)
    }
  }

  newQuestion(){
    this.questions.push(this.question)
    this.quiz.id = this.sharedService.generateRandomString(3)
  } 

  addQuestion(){
    this.question.id = this.sharedService.generateRandomString(4)
    this.question.quizId = this.quiz.id
    this.finalQuestions.push(this.question)
    this.question = {} as Question
  }


  calcDegree(){
    this.fullMark = this.quizStarted.questionDTOs.length
    if(this.degree >= this.fullMark/2){
      this.message = 'Congratolation You Are Pass'
    }else{
      this.message = 'Sorry You Are Fail'
    }
    this.sharedService.openModal()
  }

  submitQuiz(){
   if(this.role == 'Professor'){
    this.quiz.questionDTOs = this.finalQuestions
    if(this.quiz.status == 0){
      this.quiz.status = 0
    }else this.quiz.status = 1
    
    this.quizService.creatQuiz(this.quiz).subscribe({
      next: response =>{
        this.questions = []
        this.finalQuestions = []
        this.quiz = {} as Quiz
        this.toastr.success('Quiz Add' , 'Success')
      }, error : err =>{
        this.toastr.error('Quiz Add' , 'Invalid')
        console.error('add quiz' , err)
      }
    })
   }else{
    //put degree in database  and  remove quiz from student , relation m quiz to m student [degree , status]
   this.router.navigate(['home'])
   }
    this.sharedService.closeModal()
  }

  getMyCourses(){
    const nameUser = sessionStorage.getItem('userName') as string
    this.courseService.getMyCourse(nameUser).subscribe(
      (listMyCourses : Course[]) =>
        this.courses = listMyCourses
    )
  }

  getQuiz(userName : string){
    this.quizService.getQuiz(userName).subscribe(
      (data : any) =>{
        this.quizes = data 
      }
    )
  }

  nextQues(){
    this.i++
  }

  prevQues(){
    this.i--
  }

  pageNumbers(){
    let totalPage = this.quizStarted.questionDTOs.length 
    let pageNumArray = new Array(totalPage)
    return pageNumArray
  }

  changePageNumber(pageNumber : number){
    this.i = pageNumber
    }

    submitAnswer(i : number){
      if(this.answer == this.quizStarted.questionDTOs[i-1].answer){
        this.degree ++
      }
     
    this.answer= null
    }

  startQuiz(quiz : Quiz){
    this.quizStarted = quiz
  }





}
