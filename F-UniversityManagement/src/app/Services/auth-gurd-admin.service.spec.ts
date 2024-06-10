import { TestBed } from '@angular/core/testing';

import { AuthGurdAdminService } from './auth-gurd-admin.service';

describe('AuthGurdAdminService', () => {
  let service: AuthGurdAdminService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthGurdAdminService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
