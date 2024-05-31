import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { College } from 'src/app/Models/college-model';
import { Department } from 'src/app/Models/department-model';
import { Student } from 'src/app/Models/student-model';
import { CollegeService } from 'src/app/Services/College/college.service';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { StudentService } from 'src/app/Services/Student/student.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  filterStudent : Student[] = []
  students : Student[] = []
  depts : Department[] =  []
  selected : Student = {} as Student
  isEditing : boolean = false
  selectToDelete! : string
  currentPage : number = 1
  lengthDepts : number = 0
  pageSizes : Array<number> = [ 5 , 10 , 20]
  pageSize : number = this.pageSizes[0]
  imgFile! : any
  colleges : any

  constructor(public departService : DepartmentService ,private userService : UserService, public studentService : StudentService ,private collegeService : CollegeService, private toastr : ToastrService) {}

  role = sessionStorage.getItem('role')
  userName = sessionStorage.getItem('userName') as string
  user!:any

  async ngOnInit(){
    this.user = await this.getUser()
    this.getData()
    this.getColleges()
    this.getDepts()
  }

  async getUser(): Promise<any> {
    const response = await this.userService.getUser(this.userName).toPromise();
    return response;
  }

  getData(){
    this.studentService.getStudents().subscribe(
      (students : Student[]) => {
        if(this.role == 'Admin'){
           this.filterStudent = students
           this.students = students
        }else{
          for(const student of students){
            if(student.collegeId == this.user.collegeId ){
              this.students.push(student)
              this.filterStudent.push(student)
            }else{
              continue
            }
          }
        }
       
      },
      (error)=>{
        console.error("Error Fetching Students" , error)
      })
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

  addNew(){
    this.filterStudent.unshift({
      id: '',
      fName: '',
      lName:'',
      email : '',
      password:'',
      phone : '',
      birthDate:new Date(),
    address : '',
    gender : 0,
    img : '',
    levelYear : 0,
    departmentId : ''
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
    this.userService.deletUser(id).subscribe({
      next : response =>{
        this.getData()
        this.toastr.warning('College Are Deleted' , 'Success')
      }, error : err =>{
        this.toastr.error('College Are Deleted' , 'invalid')
      }
    })
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

  
  async getDepts(){
    if(this.role === 'Admin'){
      return this.depts = await this.departService.getDepartments()
    }else{
      for(const dept of await this.departService.getDepartments()){
        if(dept.collegeId === this.user.collegeId){
          this.depts.push(dept)
        }else{
          continue
        }
        
      }return this.depts
    }
  }

  filterDepts(collegeId : string){
    return this.depts.filter(dep => dep.collegeId == collegeId)
  }

  handleImageChange(event: any) {
    this.imgFile = event.target.files[0]
   }

  update(student : Student){
    //add new
      const formData = new FormData()
      formData.append('fName' , student.fName)
      formData.append('lName' , student.lName)
      formData.append('email' , student.email)
      formData.append('passwordHash' , student.password)
      formData.append('phone' , student.phone)
      formData.append('birthDate' , String(student.birthDate))
      formData.append('address' , student.address)
      formData.append('gender' , String(student.gender))
      formData.append('departmentId' , student.departmentId)
      formData.append('levelYear' , String(student.levelYear))
      formData.append('img' , this.imgFile)
      formData.append('collegeId' , this.user.collegeId)
      if(this.isEditing){
        formData.append('userName' , student.userName)
        formData.append('id' , student.id)
        this.studentService.update(formData).subscribe({
          next : response => {
            this.visibleData()
            this.toastr.success("Student Updated" , "Success")
          }, error : error => {
            this.toastr.error("Student Updated" , "Invalid")
          }
        })
      }else{
        formData.append('userName' , student.fName + '_' + student.lName + '_' + this.generateRandomString(2))
        formData.append('id' , this.generateRandomString(4))
        this.studentService.rigester(formData).subscribe({
          next : response => {
            this.visibleData()
            this.toastr.success("Student Registered" , "Success")
          }, error : error => {
            this.toastr.error("Student Registered" , "Invalid")
          }
        })
      }
      
    //}
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

  this.filterStudent = this.students.filter((item) => {
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
