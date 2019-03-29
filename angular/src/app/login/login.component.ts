import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { LoginService } from '../api/login.service';
import { RouteUrls } from '../constants/routes';
import { UserAccount } from '../models/user-account.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;

  constructor(
    private readonly router: Router,
    private readonly loginApi: LoginService) { }

  ngOnInit() {
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit() {

    const user = new UserAccount();
    user.userName = this.loginForm.controls['username'].value;
    user.password = this.loginForm.controls['password'].value;

    this.loginApi.post(user).subscribe((result: UserAccount) => {
      this.router.navigateByUrl(RouteUrls.AgentDashboardComponent);
    });

  }

}
