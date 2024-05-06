import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

// Services 
import { UserService } from '../services/user.service';

export const AdminGuard: CanActivateFn = (route, state) => {

  const userService = inject(UserService);
  const router = inject(Router);  

  if (userService.roles?.indexOf('Administrator') !== -1) {
    return true;
  } else  {
    router.navigateByUrl('/dashboard');
    return false;
  }
  
};
