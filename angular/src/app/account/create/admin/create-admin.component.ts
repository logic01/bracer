import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RouteUrls } from '../../../constants/routes';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-admin',
  templateUrl: './create-admin.component.html',
  styleUrls: ['./create-admin.component.scss']
})
export class CreateAdminComponent implements OnInit {

  public accountForm: FormGroup;

  constructor(private readonly router: Router) { }

  ngOnInit() {
    this.accountForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      password_confirm: new FormControl('', Validators.required),
      pin: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
  }
}
