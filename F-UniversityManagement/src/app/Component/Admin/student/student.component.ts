import { Component, OnInit } from '@angular/core';
import { Department } from 'src/app/Models/department-model';
import { Student } from 'src/app/Models/student-model';
import { DepartmentService } from 'src/app/Services/Department/department.service';

@Component({
  selector: 'student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  filterStudent : Student[] = []
  selected : Student = {} as Student
  isEditing : boolean = false
  selectToDelete! : string
  currentPage : number = 1
  lengthDepts : number = 0
  pageSizes : Array<number> = [ 5 , 10 , 20]
  pageSize : number = this.pageSizes[0]

  constructor(public departService : DepartmentService) {}

  ngOnInit(): void {
    this.departService.getDepartments()
  }

  addNew(){
    this.filterStudent.unshift({
      id: '',
      fName: '',
      lName:'',
      email : '',
      password:'',
      phone : '',
      dateOfBirth:'',
    address : '',
    gender : 0,
    img : '',
    levelYear : 0,
    departmentId : 0
    } as Student);
    this.selected = this.filterStudent[0]
  }

  Selecte(student : Student){
    if(Object.keys(this.selected).length === 0){
      this.selected = student
      this.isEditing = true
    }
    

  }

  openModal(id : string){
    this.selectToDelete = id
    const modal = document.getElementById('exampleModal');
    if(modal != null){
      modal.style.display = 'block' ;
    }
   }
  
   closeModal(){
    const modal = document.getElementById('exampleModal');
    if(modal != null){
      modal.style.display = 'none' ;
      this.selected = {} as Student
      this.isEditing = false
    }
   }

   onDelete(id : string){
    this.closeModal()
    //this.service.deleteCollege(id).subscribe({
      //next : response =>{
        //this.getData()
        //this.toastr.success('College Are Deleted' , 'Success')
      //}, error : err =>{
       // this.toastr.error('College Are Deleted' , 'invalis')
      //}
    //})
  }

  update(Student : Student){
    //add new
      const formData = new FormData()
      //formData.append('Name' , college.name)
      //formData.append('Description' , college.description)
      //formData.append( 'image', this.imgFile)
    //if(!this.isEditing){
      //formData.append('Id' , this.generateRandomString(4))
      //this.service.createCollege(formData).subscribe({
       // next : response =>{
         // this.getData()
          //this.toastr.success('College Are Added' , 'Success')
        //}, error : err =>{
         // this.toastr.error('College Are Added' , 'Invalid')
       // }
      //})
   // }
    //update
   // else{
     // formData.append('Id' , college.id)
      //this.service.updateCollege(formData).subscribe({
        //next : response => {
          //this.getData()
          //this.toastr.success('College Are Updated', 'Success')
        //},
        //error : err =>{
          //this.toastr.success('College Are Updated', 'Invalid')
        //}
      //})
    //}
     // clean up
     this.selected= {} as Student;
     this.isEditing = false
  }

  cancel() {
    if(!this.isEditing && confirm('changed unsaved')){
      this.filterStudent.splice(0,1)
    }
    this.selected = {} as Student;
    this.isEditing = false
}

visibleData(){
  let startIndex = (this.currentPage -1) * this.pageSize
  let endIndex = startIndex + this.pageSize
 return this.filterStudent.slice(startIndex,endIndex)
}

nextPage(){
  this.currentPage++
  this.visibleData()
}

previousPage(){
  this.currentPage--
  this.visibleData()
}


pageNumbers(){
  let totalPage = Math.ceil(this.filterStudent.length / this.pageSize)
  let pageNumArray = new Array(totalPage)
  return pageNumArray
}

changePageNumber(pageNumber : number){
this.currentPage = pageNumber
this.visibleData()
}

filterData(searchTerm: string) {

this.visibleData();
}

changePageSize(page : any){
this.pageSize = page;
this.visibleData()
}
}
