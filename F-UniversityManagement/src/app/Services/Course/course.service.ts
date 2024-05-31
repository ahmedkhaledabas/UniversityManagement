import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  url : string = environment.apiBaseUrl + '/Course'
  constructor(private http : HttpClient) { }

  getCourses():Observable<any>{
    return this.http.get<any>(this.url)
  }

  getCoursesForUser(userName : string):Observable<any>{
    return this.http.get(this.url + '/getForUser?userName=' + userName)
  }

  createCourse(data : FormData):Observable<any>{
    return this.http.post(this.url ,data)
  }

  updateCourse(id : string , data : FormData){
    return this.http.put(this.url + '/' + id , data)
  }

  deleteCourse(id : string){
    return this.http.delete(this.url + '/' + id)
  }

  getMyCourse(userName : string):Observable<any>{
    return this.http.get(this.url + '/userName?userName=' + userName)
  }
}
