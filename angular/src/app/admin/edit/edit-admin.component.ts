import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from '../../constants/routes';
import { AdminService } from 'src/app/api/admin.service';
import { Admin } from 'src/app/models/admin.model';
import { UserAccount } from 'src/app/models/user-account.model';

@Component({
  selector: 'app-edit-admin',
  templateUrl: './edit-admin.component.html',
  styleUrls: ['./edit-admin.component.scss']
})
export class EditAdminComponent implements OnInit, OnDestroy {

  public accountForm: FormGroup;
  private unsubscribe$ = new Subject();

  constructor(
    private readonly adminApi: AdminService,
    private readonly router: Router) { }

  ngOnInit() {
    this.accountForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      confirmationPassword: new FormControl('', Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required)
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit() {

    if (!this.accountForm.valid) {
      return;
    }

    const admin = this.buildAdminAccount();

    this.adminApi
      .put(admin)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newAdmin: Admin) => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });

  }

  private buildAdminAccount(): Admin {

    const admin = new Admin();
    admin.userAccount = new UserAccount();
    admin.userAccount.username = this.accountForm.controls['userName'].value;
    admin.userAccount.password = this.accountForm.controls['password'].value;
    admin.userAccount.confirmationPassword = this.accountForm.controls['confirmationPassword'].value;
    admin.firstName = this.accountForm.controls['firstName'].value;
    admin.lastName = this.accountForm.controls['lastName'].value;

    return admin;
  }
}
