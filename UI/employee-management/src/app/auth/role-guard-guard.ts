import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth-service';
import { inject } from '@angular/core';

export const roleGuardGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const expectedRole = route.data?.['role'];

  if(!(authService.isLoggedIn() && authService.getRole() === expectedRole)){
    router.navigate(['/books']);
    return false;
  }
  return true;
};