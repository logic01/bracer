import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Admin } from 'src/app/models/admin.model';
import { UserAccount } from 'src/app/models/user-account.model';

@Component({
  selector: 'app-admin-account-form',
  templateUrl: './admin-account-form.component.html',
  styleUrls: ['./admin-account-form.component.scss']
})
export class AdminAccountFormComponent implements OnInit {

  @Input() admin$: Observable<Admin>;
  @Output() formSubmitEvent = new EventEmitter<Admin>();

  public accountForm: FormGroup;

  constructor() { }

  ngOnInit() {
    this.accountForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      confirmationPassword: new FormControl('', Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required)
    });

    // populate form if we have a vendor bound to the form
    if (this.admin$) {
      this.admin$.subscribe((result: Admin) => {
        this.accountForm.patchValue(result);
        this.accountForm.patchValue(result.userAccount);
      });
    }

  }

  onSubmit() {

    if (!this.accountForm.valid) {
      return;
    }

    const admin = this.buildAdmin();

    this.formSubmitEvent.emit(admin);
  }

  private buildAdmin(): Admin {

    const admin = new Admin();
    admin.userAccount = new UserAccount();

    admin.userAccount.userName = this.accountForm.controls['userName'].value;
    admin.userAccount.password = this.accountForm.controls['password'].value;
    admin.userAccount.confirmationPassword = this.accountForm.controls['confirmationPassword'].value;
    admin.firstName = this.accountForm.controls['firstName'].value;
    admin.lastName = this.accountForm.controls['lastName'].value;

    return admin;
  }
}
