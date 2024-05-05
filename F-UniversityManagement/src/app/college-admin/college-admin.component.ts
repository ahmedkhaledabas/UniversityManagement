import { College } from './../Shared/college-detail-model';
import { Component, OnInit } from '@angular/core';
import { CollegeApiService } from '../Services/college-api.service';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';


@Component({
  selector: 'app-college-admin',
  templateUrl: './college-admin.component.html',
  styleUrls: ['./college-admin.component.css']
})
export class CollegeAdminComponent implements OnInit{
  updatedCollege : College ={} as College;
 collegeSelected: College = {} as College;
 frm!:FormGroup;
constructor(public service : CollegeApiService,private formBuilder: FormBuilder ) { }

  ngOnInit(): void {

     this.service.getColleges();
     this.frm = this.formBuilder.group({
      'id':[0],
      'collegeName':[''],
      'collegeDesc':[''],
      'imageFile' :[]
     })
  }
 
form = this.formBuilder.group({
  collegeName: ['', [Validators.required]],
  collegeDesc: ['', [Validators.required]],
  imageFile:[]
 });


 selectCollege(college: College) {
         // check if userSelected is empty, before assigning a selected user
     if(Object.keys(this.collegeSelected).length === 0) {
       this.collegeSelected = college;
 
       this.form.patchValue({
        collegeName: college.name,
        collegeDesc: college.description,
       })
     }
   }

   addCollege(){
    this.service.colleges.unshift()
    this.collegeSelected = this.service.colleges[0];
   }

cancel() {
  // clears the user selected
  this.collegeSelected = {} as College;

  // resets the form
this.form.reset();
this.service.getColleges();
}

  
  submit() {
    this.service.formSubmited = true;
    // updates the college selected
    if(this.form.valid){
      if(this.collegeSelected.id != 0){
    this.service.updateCollege({
     id : this.collegeSelected.id,
     name : this.form.value.collegeName,
     description :  this.form.value.collegeDesc
    } as College)
    // clean up
    this.collegeSelected = {} as College;
    this.form.reset(); 
}
// add new college
else{
  this.service.createCollege({
    name : this.form.value.collegeName,
    description :  this.form.value.collegeDesc,
   } as College)
   // clean up
   this.collegeSelected = {} as College;
   this.form.reset(); 
}
    }

  }

   deleteCollege(id : number){
      this.service.deleteCollege(id);
      this.cancel()
   }

   openModal(){
    const modal = document.getElementById('exampleModal');
    if(modal != null){
      modal.style.display = 'block' ;
    }
   }

   closeModal(){
    const modal = document.getElementById('exampleModal');
    if(modal != null){
      modal.style.display = 'none' ;
    }
   }


}
