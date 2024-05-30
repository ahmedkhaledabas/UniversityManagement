import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { College } from 'src/app/Models/college-model';
import { Department } from 'src/app/Models/department-model';
import { Professor } from 'src/app/Models/professor-model';
import { CollegeService } from 'src/app/Services/College/college.service';
import { DepartmentService } from 'src/app/Services/Department/department.service';
import { ProfessorService } from 'src/app/Services/Professor/professor.service';
import { UserService } from 'src/app/Services/User/user.service';

@Component({
  selector: 'app-professor',
  templateUrl: './professor.component.html',
  styleUrls: ['./professor.component.css']
})
export class ProfessorComponent implements OnInit {

  filterProf : Professor[] = []
  profs : Professor[] = []
  selected : Professor = {} as Professor
  isEditing : boolean = false
  selectToDelete! : string
  currentPage : number = 1
  lengthDepts : number = 0
  pageSizes : Array<number> = [ 5 , 10 , 20]
  pageSize : number = this.pageSizes[0]
  imgFile! : any
  colleges : any
  role = sessionStorage.getItem('role')
  userName = sessionStorage.getItem('userName') as string
  user!:any

  constructor(private professorService : ProfessorService ,private userService : UserService, private toastr : ToastrService , public departService : DepartmentService , private collegeService : CollegeService) {}

  async ngOnInit(){
    this.user = await this.getUser()
    this.departService.getDepartments()
    this.getData()
    this.getColleges()
  }

  getData(){
    this.professorService.getProfessors().subscribe(
      (professors : Professor[]) => {
        if(this.role == 'Admin'){
          this.filterProf = professors
          this.profs = professors
        }else{
          for(const prof of professors){
            if(prof.collegeId == this.user.collegeId){
              this.profs.push(prof)
              this.filterProf.push(prof)
            }else{
              continue
            }
          }
        }
       
      },
      (error) =>{
        console.error("Error Fetching Professors" , error)
      }
      )
    
  }

  async getUser(): Promise<any> {
    const response = await this.userService.getUser(this.userName).toPromise();
    return response;
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
  
  filterDepts(collegeId : string):Department[]{
  return this.departService.departments.filter(dep => dep.collegeId === collegeId)
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

  update(professor : Professor){
    //add new
      const formData = new FormData()
     
      formData.append('fName' , professor.fName)
      formData.append('lName' , professor.lName)
      formData.append('email' , professor.email)
      formData.append('password' , professor.password)
      formData.append('phone' , professor.phone)
      formData.append('birthDate' , String(professor.birthDate))
      formData.append('address' , professor.address)
      formData.append('gender' , String(professor.gender))
      formData.append('specialist' , professor.specialist)
      formData.append('rank' , String(professor.rank))
      if(this.role == 'Admin'){
        formData.append('collegeId' , professor.collegeId)
      }else{
        formData.append('collegeId' , this.user.collegeId)
      }
      formData.append('departmentId' , professor.departmentId)
      formData.append('img' , this.imgFile)
      
    if(!this.isEditing){
      formData.append('userName' , professor.fName + '_' + professor.lName + '_' + this.generateRandomString(2))
      formData.append('id' , this.generateRandomString(4))
      this.professorService.register(formData).subscribe({
        next : response => {
          this.getData()
          this.toastr.success("Professor Registered" , "Success")
        }, error : error => {
          this.toastr.error("Professor Registered" , "Invalid")
        }
      })
    }
    else{
      formData.append('userName' , professor.userName)
      formData.append('id' , professor.id)
      this.professorService.update(formData).subscribe({
        next : response => {
          this.getData()
          this.toastr.success("Professor Updated" , "Success")
        }, error : error => {
          this.toastr.error("Professor Updated" , "Invalid")
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
     this.selected= {} as Professor;
     this.isEditing = false
  }


  addNew(){
    this.filterProf.unshift({
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
    specialist : '',
    rank : 0,
    departmentId : '',
    collegeId : ''
    } as Professor);
    this.selected = this.filterProf[0]
  }

  selecte(prof : Professor){
    if(Object.keys(this.selected).length === 0){
      this.selected = prof
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
      this.selected = {} as Professor
      this.isEditing = false
    }
   }

   onDelete(id : string){
    this.closeModal()
    this.userService.deletUser(id).subscribe({
      next : response =>{
        this.getData()
        this.toastr.warning('Professor Are Deleted' , 'Success')
      }, error : err =>{
        this.toastr.error('Professor Are Deleted' , 'invalid')
      }
    })
  }


  cancel() {
    if(!this.isEditing && confirm('changed unsaved')){
      this.filterProf.splice(0,1)
    }
    this.selected = {} as Professor;
    this.isEditing = false
}

visibleData(){
  let startIndex = (this.currentPage -1) * this.pageSize
  let endIndex = startIndex + this.pageSize
 return this.filterProf.slice(startIndex,endIndex)
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
  let totalPage = Math.ceil(this.filterProf.length / this.pageSize)
  let pageNumArray = new Array(totalPage)
  return pageNumArray
}

changePageNumber(pageNumber : number){
this.currentPage = pageNumber
this.visibleData()
}

filterData(searchTerm: string) {
  this.filterProf = this.profs.filter((item) => {
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
