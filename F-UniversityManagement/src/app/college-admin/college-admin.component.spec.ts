import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CollegeAdminComponent } from './college-admin.component';

describe('CollegeAdminComponent', () => {
  let component: CollegeAdminComponent;
  let fixture: ComponentFixture<CollegeAdminComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CollegeAdminComponent]
    });
    fixture = TestBed.createComponent(CollegeAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
