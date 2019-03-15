import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUrls } from './constants/routes';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private readonly router: Router) { }

  loginClick() {
    this.router.navigateByUrl(RouteUrls.LoginComponent);
  }

  createAccountClick() {
    this.router.navigateByUrl(RouteUrls.TypeSelectionComponent);
  }

}
