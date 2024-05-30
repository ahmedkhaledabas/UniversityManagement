import { UserService } from 'src/app/Services/User/user.service';
import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/Models/employee-model';
import { EmployeeService } from 'src/app/Services/Employee/employee.service';
import { ToastrService } from 'ngx-toastr';
import { CollegeService } from 'src/app/Services/College/college.service';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { College } from 'src/app/Models/college-model';
import { Department } from 'src/app/Models/department-model';


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  filterEmps : Employee[] = []
  emps : Employee[] = []
  selected : Employee = {} as Employee
  isEditing : boolean = false
  selectToDelete! : string
  currentPage : number = 1
  lengthDepts : number = 0
  pageSizes : Array<number> = [ 5 , 10 , 20]
  pageSize : number = this.pageSizes[0]
  imgFile! : any
  colleges : any

  constructor(private empService : EmployeeService ,public departService : DepartmentService ,private toastr : ToastrService , private collegeService : CollegeService , private userService : UserService) {
  }
  ngOnInit(): void {
    this.departService.getDepartments()
    this.getColleges()
    this.getData()
  }


  getData(){
    this.empService.getEmps().subscribe(
      (emps : Employee[]) => {
        this.filterEmps = emps
        this.emps = emps
      },
      (error) =>{
        console.error("Error Fetching Professors" , error)
      }
      )
    
  }

  
  getColleges() {
    this.collegeService.getColleges().subscribe(
        (colleges: College[]) => {
            this.colleges = colleges;
        },
        (error) => {
            console.error("Error fetching colleges:", error);
        }
    );
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

   filterDepts(collegeId : string):Department[]{
    return this.departService.departments.filter(dep => dep.collegeId === collegeId)
   }

  update(emp : Employee){
    //add new
    
      const formData = new FormData()
      formData.append('fName' , emp.fName)
      formData.append('lName' , emp.lName)
      formData.append('email' , emp.email)
      formData.append('password' , emp.password)
      formData.append('phone' , emp.phone)
      formData.append('birthDate' , String(emp.birthDate))
      formData.append('address' , emp.address)
      formData.append('gender' , String(emp.gender))
      formData.append('collegeId' , emp.collegeId)
      formData.append('departmentId' , emp.departmentId)
      formData.append('empSalary' , String(emp.empSalary))
      formData.append('img' , this.imgFile)
      if(!this.isEditing){
        formData.append('userName' , emp.fName + '_' + emp.lName + '_' + this.generateRandomString(2))
      formData.append('id' , this.generateRandomString(4))
      this.empService.register(formData).subscribe({
        next : response => {
          this.getData()
          this.toastr.success("Employee Registered" , "Success")
        }, error : error => {
          this.toastr.error("Employee Registered" , "Invalid")
        }
      })
      }
      else{
        formData.append('userName' , emp.userName)
      formData.append('id' , emp.id)
      this.empService.update(formData).subscribe({
        next : response => {
          this.getData()
          this.toastr.success("Employee Updated" , "Success")
        }, error : error => {
          this.toastr.error("Employee Updated" , "Invalid")
        }
      })
    }
     // clean up
     this.selected= {} as Employee;
     this.isEditing = false
  }


  addNew(){
    this.filterEmps.unshift({
      id: '',
      fName: '',
      lName:'',
      userName:'',
      email : '',
      password:'',
      phone : '',
      birthDate : new Date(),
    address : '',
    gender : 0,
    img : '',
    collegeId : '',
    empSalary :0,
    departmentId :''
    } as Employee);
    this.selected = this.filterEmps[0]
  }

  selecte(emp : Employee){
    if(Object.keys(this.selected).length === 0){
      this.selected = emp
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
      this.selected = {} as Employee
      this.isEditing = false
    }
   }

   onDelete(id : string){
    this.closeModal()
     this.userService.deletUser(id).subscribe({
      next : response =>{
        this.getData()
        this.toastr.warning('Employee Are Deleted' , 'Success')
      }, error : err =>{
        this.toastr.error('Employee Are Deleted' , 'invalid')
      }
    })
  }


  cancel() {
    if(!this.isEditing && confirm('changed unsaved')){
      this.filterEmps.splice(0,1)
    }
    this.selected = {} as Employee;
    this.isEditing = false
}

visibleData(){
  let startIndex = (this.currentPage -1) * this.pageSize
  let endIndex = startIndex + this.pageSize
 return this.filterEmps.slice(startIndex,endIndex)
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
  let totalPage = Math.ceil(this.filterEmps.length / this.pageSize)
  let pageNumArray = new Array(totalPage)
  return pageNumArray
}

changePageNumber(pageNumber : number){
this.currentPage = pageNumber
this.visibleData()
}

filterData(searchTerm: string) {
  this.filterEmps = this.emps.filter((item) => {
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

}
