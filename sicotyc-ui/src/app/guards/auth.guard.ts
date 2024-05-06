import { inject } from '@angular/core';
import { CanActivateFn, CanMatchFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { tap } from 'rxjs';

export const CanMatch: CanMatchFn = () => {
  const router = inject(Router);
  return inject(UserService).validateToken()
    .pipe(
      tap( isAuthenticated => {
        if (!isAuthenticated) {
          // Aca se puede hacer mas mejoras
          router.navigateByUrl('/login');
        }
      })
    )
};

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
