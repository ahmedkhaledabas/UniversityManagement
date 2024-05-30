import { Component, OnInit } from '@angular/core';
import { College } from 'src/app/Models/college-model';
import { CollegeService } from 'src/app/Services/College/college.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';



@Component({
  selector: 'app-colleg',
  templateUrl: './colleg.component.html',
  styleUrls: ['./colleg.component.css']
})
export class CollegComponent implements OnInit {
  
  constructor(public service : CollegeService , private toastr : ToastrService) { 
  }

  filterColleges : College[] = []
  colleges : College[] = []
  selected : College = {} as College
  isEditing: boolean = false
  imgFile! : any
  selectToDelete! : string
  currentPage : number = 1
  lengthDepts : number = 0
  pageSizes : Array<number> = [ 5 , 10 , 20]
  pageSize : number = this.pageSizes[0]
 


  ngOnInit(): void {
    this.getData()

  }

   getData() {
    this.service.getColleges().subscribe(
        (colleges: College[]) => {
            this.filterColleges = colleges;
            this.colleges = colleges
        },
        (error) => {
            console.error("Error fetching colleges:", error);
        }
    );
  }

  visibleData(){
  let startIndex = (this.currentPage -1) * this.pageSize
  let endIndex = startIndex + this.pageSize
 return this.filterColleges.slice(startIndex,endIndex)
  }

  select(college : College){
    if(Object.keys(this.selected).length === 0){
      this.selected = college
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

  update(college : College){
    //add new
      const formData = new FormData()
      formData.append('Name' , college.name)
      formData.append('Description' , college.description)
      formData.append( 'image', this.imgFile)
    if(!this.isEditing){
      formData.append('Id' , this.generateRandomString(4))
      this.service.createCollege(formData).subscribe({
        next : response =>{
          this.getData()
          this.toastr.success('College Are Added' , 'Success')
        }, error : err =>{
          this.toastr.error('College Are Added' , 'Invalid')
        }
      })
    }
    //update
    else{
      formData.append('Id' , college.id)
      this.service.updateCollege(formData).subscribe({
        next : response => {
          this.getData()
          this.toastr.success('College Are Updated', 'Success')
          
        },
        error : err =>{
          this.toastr.error('College Are Updated', 'Invalid')
        }
      })
    }
     // clean up
     this.selected = {} as College;
     this.isEditing = false
  }

  onDelete(id : string){
    this.closeModal()
    this.service.deleteCollege(id).subscribe({
      next : response =>{
        this.getData()
        this.toastr.success('College Are Deleted' , 'Success')
      }, error : err =>{
        this.toastr.error('College Are Deleted' , 'invalis')
      }
    })
  }

  cancel() {
    if(!this.isEditing && confirm('changed unsaved')){
      this.filterColleges.splice(0,1)
    }
    this.selected = {} as College;
    this.isEditing = false
}

  addNew(){
    this.filterColleges.unshift({
      id: '',
      name: '',
      description: '',
      img: '',
    } as College);
    this.selected = this.filterColleges[0]
  }

  handleImageChange(event: any) {
   this.imgFile = event.target.files[0]
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
    this.selected = {} as College
    this.isEditing = false
  }
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
  let totalPage = Math.ceil(this.filterColleges.length / this.pageSize)
  let pageNumArray = new Array(totalPage)
  return pageNumArray
}

changePageNumber(pageNumber : number){
this.currentPage = pageNumber
this.visibleData()
}

filterData(searchTerm: string) {
  
  this.filterColleges = this.colleges.filter((item) => {
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