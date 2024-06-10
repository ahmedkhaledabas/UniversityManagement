import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorNotAccessComponent } from './error-not-access.component';

describe('ErrorNotAccessComponent', () => {
  let component: ErrorNotAccessComponent;
  let fixture: ComponentFixture<ErrorNotAccessComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ErrorNotAccessComponent]
    });
    fixture = TestBed.createComponent(ErrorNotAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
