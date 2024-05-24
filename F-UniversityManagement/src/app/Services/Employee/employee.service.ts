import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  url : string = environment.apiBaseUrl + '/Employee'
  constructor(private http : HttpClient) { }

  getEmps():Observable<any>{
    return this.http.get(this.url)
  }

  register(data : FormData):Observable<any>{
    return this.http.post(this.url , data)
  }
}
