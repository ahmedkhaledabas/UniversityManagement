import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CollegComponent } from './colleg.component';

describe('CollegComponent', () => {
  let component: CollegComponent;
  let fixture: ComponentFixture<CollegComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CollegComponent]
    });
    fixture = TestBed.createComponent(CollegComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
