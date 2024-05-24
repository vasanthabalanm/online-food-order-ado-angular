import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { adminViewGuard } from './admin-view.guard';

describe('adminViewGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => adminViewGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
