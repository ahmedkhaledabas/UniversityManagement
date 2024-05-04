import { College } from './../Shared/college-detail-model';
import { Component, OnInit } from '@angular/core';
import { CollegeApiService } from '../Services/college-api.service';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { valueOrDefault } from 'src/assets/admin/vendor/chart.js/helpers';


@Component({
  selector: 'app-college-admin',
  templateUrl: './college-admin.component.html',
  styleUrls: ['./college-admin.component.css']
})
export class CollegeAdminComponent implements OnInit{
  updatedCollege : College ={} as College;
 collegeSelected: College = {} as College;


constructor(public service : CollegeApiService,private formBuilder: FormBuilder ) { }

  ngOnInit(): void {
     this.service.getColleges();
  }
 
form = this.formBuilder.group({
    Name: ['', [Validators.required]],
    Desc: ['', [Validators.required]],
    Img :['' , [Validators.required]]
 });



 selectCollege(college: College) {
         // check if userSelected is empty, before assigning a selected user
     if(Object.keys(this.collegeSelected).length === 0) {
       this.collegeSelected = college;
 
       this.form.patchValue({
         Name: college.name,
         Desc: college.description,
         Img: college.img
       })
     }
   }

   addCollege(){
    this.service.colleges.unshift(new College)
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
     name : this.form.value.Name,
     description :  this.form.value.Desc,
     img : this.form.value.Img
    } as College)
    // clean up
    this.collegeSelected = {} as College;
    this.form.reset(); 
}
// add new college
else{
  this.service.createCollege({
    name : this.form.value.Name,
    description :  this.form.value.Desc,
    img : this.form.value.Img
   } as College );
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
