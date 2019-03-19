import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { RouteUrls } from '../../constants/routes';
import { User } from '../../models/user.model';
import { Subscription } from 'rxjs';
import { UserService } from '../../api/user.service';

@Component({
  selector: 'app-edit-physician',
  templateUrl: './edit-physician.component.html',
  styleUrls: ['./edit-physician.component.scss']
})
export class EditPhysicianComponent implements OnInit, OnDestroy {

  public accountForm: FormGroup;
  public subscription: Subscription;

  constructor(
    private readonly userApi: UserService,
    private readonly router: Router) { }

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

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onSubmit() {

    if (!this.accountForm.valid) {
      return;
    }

    const user = this.buildUser();

    this.subscription = this.userApi
      .put(user)
      .subscribe((newUser: User) => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });
  }

  private buildUser(): User {
    const user = new User();
    user.username = this.accountForm.controls['userName'].value;
    user.password = this.accountForm.controls['password'].value;
    user.confirmationPassword = this.accountForm.controls['confirmationPassword'].value;
    user.firstName = this.accountForm.controls['firstName'].value;
    user.lastName = this.accountForm.controls['lastName'].value;
    user.phoneNumber = this.accountForm.controls['phoneNumber'].value;
    user.contactFirstName = this.accountForm.controls['contactFirstName'].value;
    user.contactLastName = this.accountForm.controls['contactLastName'].value;

    return user;
  }
}
