import { TestBed } from '@angular/core/testing';

import { CollegeApiService } from './college-api.service';

describe('CollegeApiService', () => {
  let service: CollegeApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CollegeApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
