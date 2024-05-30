import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/User/user.service';
import { User } from '../Models/user-model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  user : User ={
    userName:'',
    password : '',
    rememberMe : true
  }
  constructor(private userService : UserService , private toastr : ToastrService , private router :Router) {
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  login(user : User){
    this.userService.login(user).subscribe({
      next : response =>{
        sessionStorage.setItem('role' , response.role)
        sessionStorage.setItem('userName' , response.userName)
        this.toastr.success(response.message , "Success")
        this.router.navigate(['home']).then(() => {
          window.location.reload();}
        )
      },
      error : error =>{
        this.toastr.error(error.message , "Invalid")
      }
    })
  }

 


}
