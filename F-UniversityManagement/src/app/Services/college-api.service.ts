import { College } from './../Shared/college-detail-model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { ToastrService } from 'ngx-toastr';


@Injectable({
  providedIn: 'root'
})
export class CollegeApiService {

  url : string = environment.apiBaseUrl + '/College'
  colleges : College[] = [];
  formSubmited : boolean = false

  constructor(private http : HttpClient ,  private toastr : ToastrService) { }

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



 updateCollege(college : College ){
  this.http.put(this.url + '/' + college.id , college).subscribe({
    next : response => {
      this.getColleges()
      this.toastr.success('College Are Updated', 'Success')
    },
    error : err =>{
      console.log(err)
    }
  })
 }

 createCollege(college : College){
  this.http.post(this.url,college).subscribe({
    next : response =>{
      
      this.getColleges()
      this.toastr.success('College Are Added' , 'Success')
    }, error : err =>{
      console.log(err)
    }
  })
 }

 deleteCollege(id : number){
  this.http.delete(this.url + '/' + id).subscribe({
    next : response =>{
      this.getColleges()
      this.toastr.success('College Are Deleted' , 'Success')
    }, error : err =>{
      console.log(err)
    }
  })
 }


}
