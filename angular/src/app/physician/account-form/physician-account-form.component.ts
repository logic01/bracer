import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { Address } from 'src/app/models/address.model';
import { Physician } from 'src/app/models/physician.model';
import { UserAccount } from 'src/app/models/user-account.model';

@Component({
  selector: 'app-physician-account-form',
  templateUrl: './physician-account-form.component.html',
  styleUrls: ['./physician-account-form.component.scss']
})
export class PhysicianAccountFormComponent implements OnInit {

  @Output() formSubmitEvent = new EventEmitter<Physician>();

  public accountForm: FormGroup;

  constructor() { }

  ngOnInit() {
    this.accountForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      confirmationPassword: new FormControl('', Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
      addressLineOne: new FormControl('', Validators.required),
      addressLineTwo: new FormControl(''),
      city: new FormControl('', Validators.required),
      state: new FormControl('', Validators.required),
      zip: new FormControl('', Validators.required)
    });
  }

  onSubmit() {

    if (!this.accountForm.valid) {
      return;
    }

    const physician = this.buildPhysician();

    this.formSubmitEvent.emit(physician);
  }


  private buildPhysician(): Physician {

    const physician = new Physician();
    physician.userAccount = new UserAccount();
    physician.address = new Address();

    physician.userAccount.userName = this.accountForm.controls['userName'].value;
    physician.userAccount.password = this.accountForm.controls['password'].value;
    physician.userAccount.confirmationPassword = this.accountForm.controls['confirmationPassword'].value;

    physician.firstName = this.accountForm.controls['firstName'].value;
    physician.lastName = this.accountForm.controls['lastName'].value;
    physician.phoneNumber = this.accountForm.controls['phoneNumber'].value;

    physician.address.addressLineOne = this.accountForm.controls['addressLineOne'].value;
    physician.address.addressLineTwo = this.accountForm.controls['addressLineTwo'].value;
    physician.address.city = this.accountForm.controls['city'].value;
    physician.address.state = this.accountForm.controls['state'].value;
    physician.address.zipCode = this.accountForm.controls['zip'].value;

    return physician;
  }
}
