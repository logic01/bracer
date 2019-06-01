import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUrls } from './constants/routes';
import { SessionService } from './services/session.service';
import { LogoutService } from './services/logout.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  public loggedIn$ = this.session.loggedIn$;

  constructor(
    private readonly session: SessionService,
    private readonly logoutService: LogoutService) {

  }

  singOutClick() {
    this.logoutService.logout();
  }
}
