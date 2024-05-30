import { UpdateMode } from './../../../assets/admin/vendor/chart.js/types/index.d';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Department } from 'src/app/Models/department-model';
import { environment } from 'src/environments/environment.development';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  url : string = environment.apiBaseUrl + '/Department'
  departments : Department[] = []
  constructor(private http : HttpClient , private tostar : ToastrService) { }


  async getDepartments(): Promise<Department[]> {
    try {
      const response = await this.http.get(this.url).toPromise();
      this.departments = response as Department[];
      return this.departments;
    } catch (err) {
      console.error(err);
      throw err; // or return an error response
    }
  }


deleteDepartment(id : string){
  this.http.delete(this.url + '/' + id ).subscribe({
    next : response =>{
      this.tostar.warning('Department Removed' , 'Success')
     //this.getDepartments()
    },
    error : err =>{
      this.tostar.error('Department Removed' , 'Invalid')
    }
  })
  }

updateDepartment(dept : Department){
  this.http.put(this.url , dept).subscribe({
    next : response =>{
      this.tostar.info('Department Updated' , 'Success')
    },
    error : err =>{
      this.tostar.error('Department Updated' , 'Invalid')
    }
  })
}

createDepartment(dept : Department){
  this.http.post(this.url , dept).subscribe({
    next : response =>{
      this.tostar.success('Department Added' , 'Success')
      this.getDepartments()
    },
    error : err =>{
      this.tostar.error('Department Added' , 'Invalid')
    }
  })
}
}


