import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/User/user.service';

@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit{
  
  role = sessionStorage.getItem('role')
  user! : any
  constructor(private userService : UserService) {}

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

}
