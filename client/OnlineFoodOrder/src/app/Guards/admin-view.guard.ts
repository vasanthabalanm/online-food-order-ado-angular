import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AdminServiceService } from '../Services/admin-service.service';

export const adminViewGuard: CanActivateFn = (route, state) => {
  let adminser = inject(AdminServiceService);
  let router = inject(Router)

  if (adminser.isLogin()) {
    return true;
  }
  else {
    router.navigate(['login']);
    return false;
  }
  return true;
};
