import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  url : string = environment.apiBaseUrl + '/Student'
  constructor(private http : HttpClient) { }

  getStudents():Observable<any>{
    return this.http.get<any>(this.url + '/GetStudents')
  }

  rigester(data : FormData) : Observable<any>{
    return this.http.post(this.url + '/Register' , data)
  }

  update(data : FormData){
    return this.http.put(this.url , data)
  }

}
