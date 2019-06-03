import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Address } from 'src/app/models/address.model';
import { Physician } from 'src/app/models/physician.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { FormatHelperService } from 'src/app/services/format-helper.service';
import { MaskService } from 'src/app/services/mask.service';
import { CustomValidators } from 'src/app/validators/custom-validators';

@Component({
  selector: 'app-physician-account-form',
  templateUrl: './physician-account-form.component.html',
  styleUrls: ['./physician-account-form.component.scss']
})
export class PhysicianAccountFormComponent implements OnInit {

  @Input() physician$: Observable<Physician>;
  @Output() formSubmitEvent = new EventEmitter<Physician>();

  public accountForm: FormGroup;

  constructor(
    public readonly maskService: MaskService,
    public readonly formatHelper: FormatHelperService) {

  }

  ngOnInit() {
    this.accountForm = new FormGroup({
      userName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      password: new FormControl('', [CustomValidators.password(6, 20), Validators.required]),
      confirmationPassword: new FormControl('', [CustomValidators.password(6, 20), Validators.required]),
      emailAddress: new FormControl('', [Validators.required, Validators.maxLength(100), CustomValidators.emailAddress]),
      firstName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      lastName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      phoneNumber: new FormControl('', [CustomValidators.phonenumber, Validators.required]),
      addressLineOne: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      addressLineTwo: new FormControl('', Validators.maxLength(100)),
      city: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      state: new FormControl('', [CustomValidators.state, Validators.required]),
      zipCode: new FormControl('', [CustomValidators.zip, Validators.required]),
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

    // populate form if we have a physician bound to the form
    if (this.physician$) {
      this.physician$.subscribe((result: Physician) => {
        this.accountForm.patchValue(result);
        this.accountForm.patchValue(result.userAccount);
        this.accountForm.patchValue(result.address);
      });
    }
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
    physician.userAccount.emailAddress = this.accountForm.controls['emailAddress'].value;
    physician.userAccount.active = this.accountForm.controls['active'].value;

    physician.firstName = this.accountForm.controls['firstName'].value;
    physician.lastName = this.accountForm.controls['lastName'].value;
    physician.phoneNumber = this.formatHelper.toNumbersOnly(this.accountForm.controls['phoneNumber'].value);

    physician.address.addressLineOne = this.accountForm.controls['addressLineOne'].value;
    physician.address.addressLineTwo = this.accountForm.controls['addressLineTwo'].value;
    physician.address.city = this.accountForm.controls['city'].value;
    physician.address.state = this.accountForm.controls['state'].value;
    physician.address.zipCode = this.accountForm.controls['zipCode'].value;

    return physician;
  }
}
