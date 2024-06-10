import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessorCoursesComponent } from './professor-courses.component';

describe('ProfessorCoursesComponent', () => {
  let component: ProfessorCoursesComponent;
  let fixture: ComponentFixture<ProfessorCoursesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProfessorCoursesComponent]
    });
    fixture = TestBed.createComponent(ProfessorCoursesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
