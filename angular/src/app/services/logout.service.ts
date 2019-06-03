import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { RouteUrls } from '../constants/routes';
import { SessionService } from './session.service';

@Injectable({
  providedIn: 'root'
})
export class LogoutService {

  constructor(
    private readonly session: SessionService,
    private readonly router: Router
    ) { }

  logout() {
    this.session.clear();
    this.router.navigateByUrl(RouteUrls.LoginComponent);
  }
}
