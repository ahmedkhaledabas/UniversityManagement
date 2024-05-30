import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from 'src/app/Models/student-model';
import { User } from 'src/app/Models/user-model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  url : string = environment.apiBaseUrl + '/User'
  constructor(private http : HttpClient) { }

  login(user : User):Observable<any>{
    return this.http.post(this.url + '/Login' , user)
  }

  logout():Observable<any>{
   return this.http.get(this.url + '/logout')
  }

  getUser(userName : string):Observable<any>{
    return this.http.get(this.url + '/userName?userName=' + userName)
  }

  deletUser(id : string){
    return this.http.delete(this.url + '?id=' + id)
  }
}
