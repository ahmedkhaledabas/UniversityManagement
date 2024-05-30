import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CollegeService } from 'src/app/Services/College/college.service';
import { College } from '../Models/college-model';
import { Professor } from '../Models/professor-model';
import { ProfessorService } from '../Services/Professor/professor.service';

@Component({
  selector: 'college',
  templateUrl: './college.component.html',
  styleUrls: ['./college.component.css']
})
export class CollegeComponent implements OnInit {

  filterColleges : College[] = []
  profs : Professor[] =[]

  constructor(public service : CollegeService , private toastr : ToastrService, private profService : ProfessorService) { 
    
  }

  ngOnInit(): void {
    this.getData()
    this.getProfessors()
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
