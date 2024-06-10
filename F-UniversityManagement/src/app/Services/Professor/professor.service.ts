import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
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

  update(data : FormData){
    return this.http.put(this.url , data)
  }

  getProf(profId : string):Observable<any>{
    return this.http.get(this.url + '/id?id=' + profId)
  }
}
