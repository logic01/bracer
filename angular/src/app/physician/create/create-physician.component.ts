import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { RouteUrls } from '../../constants/routes';
import { UserService } from '../../api/user.service';
import { User } from '../../models/user.model';
import { Subscription, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-create-physician',
  templateUrl: './create-physician.component.html',
  styleUrls: ['./create-physician.component.scss']
})
export class CreatePhysicianComponent implements OnInit, OnDestroy {

  public accountForm: FormGroup;
  private unsubscribe$ = new Subject();

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
   this.unsubscribe$.unsubscribe();
  }

  onSubmit() {

    if (!this.accountForm.valid) {
      return;
    }

    const user = this.buildUser();

    this.userApi
      .post(user)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newUser: User) => {
        this.router.navigateByUrl(RouteUrls.PhysicianDashboardComponent);
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
