import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/Models/course-model';
import { DepartmentService } from 'src/app/Services/Department/department.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  filterCourse : Course[] = []
  selected : Course = {} as Course
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
    this.filterCourse.unshift({
      id :'',
    levelYear : 0,
    name : '',
    description : '',
    img : '',
    professorId : '',
    departmentId : 0
    } as Course)
    this.selected = this.filterCourse[0]

  }
  selecte(course : Course){
    if(Object.keys(this.selected).length === 0 ){
      this.selected = course
      this.isEditing = true
    }
  }

  update(course : Course){
     // clean up
     this.selected= {} as Course;
     this.isEditing = false
  }

  cancel() {
    if(!this.isEditing && confirm('changed unsaved')){
      this.filterCourse.splice(0,1)
    }
    this.selected = {} as Course;
    this.isEditing = false
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
    this.selected = {} as Course
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


visibleData(){
  let startIndex = (this.currentPage -1) * this.pageSize
  let endIndex = startIndex + this.pageSize
 return this.filterCourse.slice(startIndex,endIndex)
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
  let totalPage = Math.ceil(this.filterCourse.length / this.pageSize)
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
