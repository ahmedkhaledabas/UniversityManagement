import { Component, OnInit } from '@angular/core';
import { Professor } from 'src/app/Models/professor-model';
import { ProfessorService } from 'src/app/Services/Professor/professor.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-professors',
  templateUrl: './professors.component.html',
  styleUrls: ['./professors.component.css']
})
export class ProfessorsComponent implements OnInit{
  
  role = sessionStorage.getItem('role')
  userName = sessionStorage.getItem('userName') as string
  profs : Professor[] = []
  user : any
 
  constructor(private profService : ProfessorService,private userService : UserService) {}

  ngOnInit(): void {
    this.getUser()
    this.getProfs()
  }

  filterProf(){
    if(this.role){
      return this.profs.filter(p=>p.collegeId == this.user.collegeId)
    }else return this.profs
  }
  
  getUser(){
    this.userService.getUser(this.userName).subscribe({
      next : response => {
        this.user = response
      }
    })
  }
  
  getProfs(){
    this.profService.getProfessors().subscribe({
      next : response => {
        this.profs = response
      },error : err => {
        console.error('fetch professors' , err)
      }
    })
  }
}
