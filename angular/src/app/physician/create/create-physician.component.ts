import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { RouteUrls } from '../../constants/routes';

@Component({
  selector: 'app-create-physician',
  templateUrl: './create-physician.component.html',
  styleUrls: ['./create-physician.component.scss']
})
export class CreatePhysicianComponent implements OnInit {

  public accountForm: FormGroup;

  constructor(private readonly router: Router) { }

  ngOnInit() {
    this.accountForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      confirmationPassword: new FormControl('', Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
      contactFirstName: new FormControl('', Validators.required),
      contactLastName: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    this.router.navigateByUrl(RouteUrls.PhysicianDashboardComponent);
  }

}
