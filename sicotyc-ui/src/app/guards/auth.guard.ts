import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { tap } from 'rxjs';

export const AuthGuard: CanActivateFn = (route, state) => {

  const userService = inject(UserService);
  const router = inject(Router);  

  return userService.validateToken()
  .pipe(
    tap( isAuthenticated => {
      if (!isAuthenticated) {
        // Aca se puede hacer mas mejoras
        router.navigateByUrl('/login');
      }
    })
  );
};
