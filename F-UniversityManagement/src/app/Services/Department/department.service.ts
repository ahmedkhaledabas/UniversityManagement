import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Department } from 'src/app/Models/department-model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  url : string = environment.apiBaseUrl + '/Department'
  departments : Department[] = []
  constructor(private http : HttpClient) { }

  getDepartments(){
    this.http.get(this.url).subscribe({
      next : response => {
        this.departments = response as Department[]
      },
      error : err =>{
        console.log(err)
      }
    })
  }

}
