import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { College } from 'src/app/Models/college-model';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class CollegeService {

  url : string = environment.apiBaseUrl + '/College';
  
  formSubmited : boolean = false
  constructor(private http : HttpClient) { }

  getColleges(): Observable<any> {
    return this.http.get<any>(this.url);
}


updateCollege(data : FormData ) : Observable<any>{
     return this.http.put(this.url + '/' + data.get('Id') , data)
 }

 createCollege(data : FormData) : Observable<any>{
     return this.http.post(this.url,data)
 }

 deleteCollege(id : string) : Observable<any>{
   return this.http.delete(this.url + '/' + id)
 }


}
