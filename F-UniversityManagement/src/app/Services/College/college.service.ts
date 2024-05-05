import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { College } from 'src/app/Models/college-model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CollegeService {

  url : string = environment.apiBaseUrl + 'College';
  colleges : College[] = []

  constructor(private http : HttpClient) { }

  getColleges(){
    this.http.get(this.url).subscribe({
     next : response =>{
       this.colleges = response as College[]
     },
     error : err => {
       console.log(err)
     }
    });
   }
}
