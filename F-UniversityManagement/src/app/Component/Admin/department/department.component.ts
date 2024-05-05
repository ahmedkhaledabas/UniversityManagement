import { Component, OnInit } from '@angular/core';
import { DepartmentService } from 'src/app/Services/Department/department.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  constructor(public service: DepartmentService) {}

  ngOnInit() {
    this.service.getDepartments();
  }

}
