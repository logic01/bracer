import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RouteUrls } from '../constants/routes';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {

  constructor(private readonly router: Router) { }

  ngOnInit() {
  }

  create_account_button_click() {
    this.router.navigateByUrl(RouteUrls.TypeSelectionComponent);
  }

  login_button_click() {
    this.router.navigateByUrl(RouteUrls.LoginComponent);
  }

}
