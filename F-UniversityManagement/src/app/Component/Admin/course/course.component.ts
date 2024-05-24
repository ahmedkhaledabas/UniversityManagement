import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Course } from 'src/app/Models/course-model';
import { Professor } from 'src/app/Models/professor-model';
import { CourseService } from 'src/app/Services/Course/course.service';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { ProfessorService } from 'src/app/Services/Professor/professor.service';

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
  professors : any
  imgFile : any

  constructor(public departService : DepartmentService , public profService : ProfessorService , public courseService : CourseService , private toastr : ToastrService) {}

  ngOnInit(): void {
    this.departService.getDepartments()
    this.getProfessors()
    this.getData()
  }


  getData(){
    this.courseService.getCourses().subscribe(
      (courses : Course[]) =>{
        this.filterCourse = courses
      },
    (error) =>{
      console.error("Featching Courses" , error)
    }
    )
  }

  getProfessors(){
    this.profService.getProfessors().subscribe(
      (professors : Professor[]) => {
        this.professors = professors
      },
      (error) => {
        console.error("Fetching Professors" , error)
      }
    )
  }

  addNew(){
    this.filterCourse.unshift({
      id :'',
    levelYear : 0,
    name : '',
    description : '',
    img : '',
    professorId : '',
    departmentId : ''
    } as Course)
    this.selected = this.filterCourse[0]

  }
  selecte(course : Course){
    if(Object.keys(this.selected).length === 0 ){
      this.selected = course
      this.isEditing = true
    }
  }

  generateRandomString(length : number){
    const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'
    let random = ''
    const characterLength = chars.length
    for(let i = 0 ; i < length ; i++){
      random+= chars.charAt(Math.floor(Math.random() * characterLength))
    }
    return random
  }

  handleImageChange(event: any) {
    this.imgFile = event.target.files[0]
   }

  update(course : Course){
    const formData = new FormData()
    formData.append('id', this.generateRandomString(4))
    formData.append('name' , course.name)
    formData.append('levelYear' , String(course.levelYear))
    formData.append('description' , course.description)
    formData.append('departmentId' , course.departmentId)
    formData.append('professorId' , course.professorId)
    formData.append('img' , this.imgFile)
    this.courseService.createCourse(formData).subscribe({
      next : response =>{
        this.getData()
        this.toastr.success("Course Added" , "Success")
      },
      error : error =>{
        this.toastr.error("Course Added" , "Invalid")
      }
    })
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
