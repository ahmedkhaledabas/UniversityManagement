import { Component, OnInit } from '@angular/core';
import { Student } from '../Models/student-model';
import { UserService } from '../Services/User/user.service';
import { User } from '../Models/user-model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { College } from 'src/app/Models/college-model';
import { CollegeService } from '../Services/College/college.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{
  user!: any
  college : College = {} as  College
  role : any
  userName : any
constructor(private userService : UserService , private toastr : ToastrService, private router :Router , private collegeService : CollegeService) {
}

async ngOnInit(): Promise<void> {
  this.role = sessionStorage.getItem('role');
  this.userName = sessionStorage.getItem('userName');
  this.user = await this.getUser();
  this.getCollege()
  //console.log(this.user)
}

async getUser(): Promise<any> {
  const response = await this.userService.getUser(this.userName).toPromise();
  return response;
}

logout(){
  this.userService.logout().subscribe({
    next : response =>{
      this.toastr.success("User Logout" , "Success")
      localStorage.clear()
      sessionStorage.clear()
      this.router.navigate(['home']).then(() => {
        window.location.reload();}
       )
    },
    error : error =>{
      this.toastr.error("User Logout" , "Invalid")
    }
  })
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