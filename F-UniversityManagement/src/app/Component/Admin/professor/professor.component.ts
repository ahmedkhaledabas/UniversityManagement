import { Component, OnInit } from '@angular/core';
import { Professor } from 'src/app/Models/professor-model';

@Component({
  selector: 'app-professor',
  templateUrl: './professor.component.html',
  styleUrls: ['./professor.component.css']
})
export class ProfessorComponent implements OnInit {

  filterProf : Professor[] = []
  selected : Professor = {} as Professor
  isEditing : boolean = false
  selectToDelete! : string
  currentPage : number = 1
  lengthDepts : number = 0
  pageSizes : Array<number> = [ 5 , 10 , 20]
  pageSize : number = this.pageSizes[0]

  constructor() {}

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  addNew(){
    this.filterProf.unshift({
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
    specialist : '',
    rank : 0
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
    //this.service.deleteCollege(id).subscribe({
      //next : response =>{
        //this.getData()
        //this.toastr.success('College Are Deleted' , 'Success')
      //}, error : err =>{
       // this.toastr.error('College Are Deleted' , 'invalis')
      //}
    //})
  }

  update(prof : Professor){
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
     this.selected= {} as Professor;
     this.isEditing = false
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

this.visibleData();
}

changePageSize(page : any){
this.pageSize = page;
this.visibleData()
}

}
