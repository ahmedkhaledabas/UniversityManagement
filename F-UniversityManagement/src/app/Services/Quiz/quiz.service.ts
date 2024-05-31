import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Quiz } from 'src/app/Models/quiz-model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  qus : Quiz[]=[]
  second!:number
  timer!:any
  qnProgress! :number;
  url = environment.apiBaseUrl + '/Quiz'
  constructor(private http : HttpClient) { }

  getQuestions(userName : string){
    return this.http.get(this.url + '/' + userName)
  }

  creatQuestion(data : Quiz){
    return this.http.post(this.url , data)
  }
}
