import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Admin } from 'src/app/models/admin.model';
import { AccountType } from 'src/app/models/enums/account-type.enum';
import { UserAccount } from 'src/app/models/user-account.model';
import { CustomValidators } from 'src/app/validators/custom-validators';

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
      userName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      password: new FormControl('', [CustomValidators.password(6, 20), Validators.required]),
      emailAddress: new FormControl('', [Validators.required, Validators.maxLength(100), CustomValidators.emailAddress]),
      confirmationPassword: new FormControl('', [CustomValidators.password(6, 20), Validators.required]),
      firstName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      lastName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      active: new FormControl(true)
    });

    this.accountForm.get('password').validator = Validators.compose([
      this.accountForm.get('password').validator,
      CustomValidators.equal(this.accountForm.get('confirmationPassword'))
    ]);

    this.accountForm.get('confirmationPassword').validator = Validators.compose([
      this.accountForm.get('confirmationPassword').validator,
      CustomValidators.equal(this.accountForm.get('password'))
    ]);


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

    admin.userAccount.type = AccountType.Admin;
    admin.userAccount.userName = this.accountForm.controls['userName'].value;
    admin.userAccount.password = this.accountForm.controls['password'].value;
    admin.userAccount.confirmationPassword = this.accountForm.controls['confirmationPassword'].value;
    admin.userAccount.emailAddress = this.accountForm.controls['emailAddress'].value;
    admin.userAccount.active = this.accountForm.controls['active'].value;

    admin.firstName = this.accountForm.controls['firstName'].value;
    admin.lastName = this.accountForm.controls['lastName'].value;

    return admin;
  }
}
