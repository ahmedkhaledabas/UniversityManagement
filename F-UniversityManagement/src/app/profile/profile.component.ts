import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/User/user.service';
import { CollegeService } from '../Services/College/college.service';
import { College } from '../Models/college-model';
import { DepartmentService } from '../Services/Department/department.service';
import { Department } from '../Models/department-model';

@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit{
  
  role = sessionStorage.getItem('role')
  user! : any
  college : College = {} as College
  dept : Department = {} as Department
  constructor(private userService : UserService, private collegeService : CollegeService, private deptService :DepartmentService) {}

  ngOnInit(): void {
    
    this.getUser()
  }

  getUser(){
    const userName = sessionStorage.getItem('userName') as string
    this.userService.getUser(userName).subscribe(response =>{
      this.user = response
    })
    console.log(this.user)
  }

  getCollege(){
    this.collegeService.getCollegeById(this.user.collegeId).subscribe(
      {
        next : response =>{
          this.college = response as College
        },
        error : err => {
          console.error('featch college')
        }
      }
    )
    return this.college.name
  }

  getDept(){
    this.deptService.getDepartmentById(this.user.departmentId).subscribe({
      next : response =>{
        this.dept = response
      }
    })
    return this.dept.name
  }
}
