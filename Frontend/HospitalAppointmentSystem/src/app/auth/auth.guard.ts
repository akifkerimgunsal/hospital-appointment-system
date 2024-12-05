import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.authService.isLoggedIn()) {
      const userRole = this.authService.getUserRole();
      if (route.data['roles'] && !route.data['roles'].includes(userRole)) {
        // Kullanıcı doğru role sahip değilse ana sayfaya yönlendirme
        this.router.navigate(['/']);
        return false;
      }
      return true;
    } else {
      // Kullanıcı giriş yapmamışsa login sayfasına yönlendirme
      this.router.navigate(['/login']);
      return false;
    }
  }
}
