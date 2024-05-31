import { Component, OnInit } from '@angular/core';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { Department } from 'src/app/Models/department-model';
import { FormBuilder, Validators } from '@angular/forms';
import { CollegeService } from 'src/app/Services/College/college.service';
import { College } from 'src/app/Shared/college-detail-model';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {
 
  constructor(public service: DepartmentService,private userService : UserService ,public collegeService : CollegeService, private fb : FormBuilder,private toastr : ToastrService ) {}

  filterDepartments: Department[] =  []
  colleges : College[] = []
  currentPage : number = 1
  lengthDepts : number = 0
  pageSizes : Array<number> = [ 5 , 10 , 20]
  pageSize : number = this.pageSizes[0]
  oldDepartment : any
  selecteDeptId : any

    user! : any 
     role = sessionStorage.getItem('role')
  userName = sessionStorage.getItem('userName') as string

 async ngOnInit() {
    
    this.getColleges() 
    this.user = await this.getUser();
    this.getData()
  }

  getColleges(){
    this.collegeService.getColleges().subscribe(
      (c : College[]) => {
          this.colleges = c
    })

  }

  async getUser(): Promise<any> {
    const response = await this.userService.getUser(this.userName).toPromise();
    return response;
  }

  async getData() {
    if(this.role === 'Admin'){
      return this.filterDepartments = await this.service.getDepartments()
    }else{
      for(const dept of await this.service.getDepartments()){
        if(dept.collegeId === this.user.collegeId){
          this.filterDepartments.push(dept)
        }else{
          continue
        }
        
      }return this.filterDepartments
    }
    
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

openModal(id : string){
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

 generateRandomString(length : number){
  const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'
  let random = ''
  const characterLength = chars.length
  for(let i = 0 ; i < length ; i++){
    random+= chars.charAt(Math.floor(Math.random() * characterLength))
  }
  return random
}

 onUpdate(dept : Department){
  if(dept.id != null){
    this.service.updateDepartment(dept)
  }else{
    dept.id = this.generateRandomString(4)
    this.service.createDepartment(dept)
    this.getColleges()

  }
 }

  onDelete(){
    this.service.deleteDepartment(this.selecteDeptId);
    this.closeModal()
   }
}
