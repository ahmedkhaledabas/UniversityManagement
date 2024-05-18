import { Component, OnInit } from '@angular/core';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { Department } from 'src/app/Models/department-model';
import { FormBuilder, Validators } from '@angular/forms';
import { CollegeService } from 'src/app/Services/College/college.service';
import { College } from 'src/app/Shared/college-detail-model';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {
 
  constructor(public service: DepartmentService ,public collegeService : CollegeService, private fb : FormBuilder ) {}

  filterDepartments: Department[] =  []
  colleges : College[] = []
  currentPage : number = 1
  lengthDepts : number = 0
  pageSizes : Array<number> = [ 5 , 10 , 20]
  pageSize : number = this.pageSizes[0]
  oldDepartment : any
  selecteDeptId : any

 async ngOnInit() {
    this.getData()
    this.getColleges()
  }
  getColleges(){
    this.collegeService.getColleges().subscribe(
      (c : College[]) => {
      this.colleges = c
    })
  }
  async getData() {
    return this.filterDepartments = await this.service.getDepartments()
  }

  visibleData(){
    let startIndex = (this.currentPage -1) * this.pageSize
    let endIndex = startIndex + this.pageSize
   return this.filterDepartments.slice(startIndex,endIndex)
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
    let totalPage = Math.ceil(this.filterDepartments.length / this.pageSize)
    let pageNumArray = new Array(totalPage)
    return pageNumArray
  }

changePageNumber(pageNumber : number){
  this.currentPage = pageNumber
  this.visibleData()
}

filterData(searchTerm: string) {
  this.filterDepartments = this.service.departments.filter((item) => {
    return Object.values(item).some((val) => {
      if (typeof val === 'string') {
        return val.toLowerCase().includes(searchTerm.toLowerCase());
      }
      return false;
    });
  });
  this.visibleData();
}

changePageSize(page : any){
this.pageSize = page;
this.visibleData()
}

onEdite(dept : Department){
this.oldDepartment = JSON.stringify(dept)
  this.filterDepartments.forEach(element =>{
    element.isEdite = false
  })
  dept.isEdite = true;
}

onCancelEdit(dept : Department){
  const oldDept = JSON.parse(this.oldDepartment)
  dept.name = oldDept.name
  dept.description = oldDept.description
  dept.collegeId = oldDept.collegeId
  dept.isEdite = false;
  //this.visibleData();
}

validateFiled(item : any){
  if(item == '' || item == null){
    return true
  }else return false
}

validateDeptName(name : string){
  if(name === ''){
    return 'Required'
  }
  else if(name.length < 4){
    return 'Min 4 Char'
  }
  else{
    return ''
  }
}
validateForm(dept : Department){
  if(dept.collegeId == null || dept.description == '' || dept.name == ''){
    return true
  }else return false
}

openModal(id : number){
  this.selecteDeptId = id;
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

 onAdd(){
  const newDept : Department = {} as Department
  newDept.isEdite = true
  newDept.name = ''
  this.filterDepartments.unshift(newDept)
 }

 onUpdate(dept : Department){
  if(dept.id != null){
    this.service.updateDepartment(dept)
  }else{
    this.service.createDepartment(dept)
  }
 }

  onDelete(deptId : number){
    this.service.deleteDepartment(deptId);
   }
}