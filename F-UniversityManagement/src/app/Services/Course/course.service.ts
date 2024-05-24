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

  createCourse(data : FormData):Observable<any>{
    return this.http.post(this.url ,data)
  }
}
