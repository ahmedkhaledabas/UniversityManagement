import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProfessorService {

  url : string = environment.apiBaseUrl + '/Professor'
  constructor(private http : HttpClient) { }

  getProfessors():Observable<any>{
    return this.http.get(this.url + '/GetProfessors')
  }

  register(data : FormData):Observable<any>{
    return this.http.post(this.url + '/Register' , data)
  }
}
