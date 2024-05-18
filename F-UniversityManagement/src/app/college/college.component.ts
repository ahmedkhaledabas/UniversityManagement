import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CollegeService } from 'src/app/Services/College/college.service';
import { College } from '../Models/college-model';

@Component({
  selector: 'college',
  templateUrl: './college.component.html',
  styleUrls: ['./college.component.css']
})
export class CollegeComponent implements OnInit {

  filterColleges : College[] = []

  constructor(public service : CollegeService , private toastr : ToastrService) { 
    
  }

  ngOnInit(): void {
    this.getData()
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
