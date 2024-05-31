import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CollegeService } from 'src/app/Services/College/college.service';
import { College } from '../Models/college-model';
import { Professor } from '../Models/professor-model';
import { ProfessorService } from '../Services/Professor/professor.service';
import { DepartmentService } from '../Services/Department/department.service';
import { Department } from '../Models/department-model';

@Component({
  selector: 'college',
  templateUrl: './college.component.html',
  styleUrls: ['./college.component.css']
})
export class CollegeComponent implements OnInit {

  filterColleges : College[] = []
  profs : Professor[] =[]

  constructor(public departService : DepartmentService ,public service : CollegeService , private toastr : ToastrService, private profService : ProfessorService) { 
    
  }

  ngOnInit(): void {
    this.getData()
    this.getProfessors()
    this.departService.getDepartments()
  }

  getDepts(collegeId : string){
    return this.departService.departments.filter(d=>d.collegeId == collegeId)
  }

  getProfessors(){
    this.profService.getProfessors().subscribe(
      (listProf : Professor[]) =>
        this.profs = listProf
    )
  }
  getData() {
    this.service.getColleges().subscribe(
        (colleges: College[]) => {
            this.filterColleges = colleges;
        },
        (error) => {
            console.error("Error fetching colleges:", error);
        }
    );
}
}
