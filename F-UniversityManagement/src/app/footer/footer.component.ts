import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/User/user.service';
import { CollegeService } from '../Services/College/college.service';
import { College } from '../Models/college-model';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit{

  role = sessionStorage.getItem('role')
  userName : any
  user!: any
  college : College = {} as  College
  constructor(private userService : UserService , private collegeService : CollegeService) {}

  async ngOnInit() {
    this.userName = sessionStorage.getItem('userName');
  this.user = await this.getUser();
  this.getCollege()
  }
  

  async getUser(): Promise<any> {
    const response = await this.userService.getUser(this.userName).toPromise();
    return response;
  }

  getCollege(){
  
    this.collegeService.getCollegeById(this.user.collegeId).subscribe({
      next : response => {
        this.college = response as College
      },
      error : err =>{
        console.error('fetch College' , err)
      }
    })
  }
}
