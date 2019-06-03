import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { DmaDialogComponent } from '../dma-dialog/dma-dialog.component';
import { Address } from 'src/app/models/address.model';
import { Patient } from 'src/app/models/patient.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { PatientService } from 'src/app/services/api/patient.service';
import { SessionService } from 'src/app/services/session.service';
import { SelectValueService } from 'src/app/services/select-value.service'
import { MaskService } from 'src/app/services/mask.service'
import { CustomValidators } from 'src/app/validators/custom-validators';

@Component({
  selector: 'app-create-patient',
  templateUrl: './create-patient.component.html',
  styleUrls: ['./create-patient.component.scss']
})
export class CreatePatientComponent implements OnInit, OnDestroy {

  form: FormGroup;
  private unsubscribe$ = new Subject();
  private agentId: string;
  statesList: string[] = SelectValueService.states;
  constructor(
    private readonly session: SessionService,
    private readonly dialog: MatDialog,
    private readonly patientApi: PatientService,
    private readonly router: Router,
    public readonly maskService: MaskService) { }

  ngOnInit() {

    /* only an agent can access this page */
    this.session.userAccount$.subscribe((result: UserAccount) => {
      this.agentId = result.userAccountId;
    });

    this.form = new FormGroup({
      firstName: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      middleName: new FormControl('', Validators.maxLength(100)),
      lastName: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      dateOfBirth: new FormControl('', [Validators.required]),
      phoneNumber: new FormControl('', [Validators.required, CustomValidators.phonenumber]),
      addressLineOne: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      addressLineTwo: new FormControl('', Validators.maxLength(100)),
      city: new FormControl('', [Validators.required, Validators.maxLength(30)]),
      state: new FormControl('', Validators.required),
      zip: new FormControl('', [Validators.required, Validators.maxLength(10), CustomValidators.zip]),
      sex: new FormControl('', Validators.required),
      language: new FormControl('English', Validators.required),
      callBackImmediately: new FormControl(false, Validators.required),
      bestTimeToCallBack: new FormControl('', Validators.required),

      pharmacy: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      medications: new FormControl('', Validators.maxLength(100)),
      notes: new FormControl('', Validators.maxLength(100)),
      physicianName: new FormControl('', Validators.maxLength(100)),
      physicianPhoneNumber: new FormControl('', CustomValidators.phonenumber),
      physicianAddressLineOne: new FormControl('', Validators.maxLength(100)),
      physicianAddressLineTwo: new FormControl('', Validators.maxLength(100)),
      physicianCity: new FormControl('', Validators.maxLength(30)),
      physicianState: new FormControl(''),
      physicianZip: new FormControl('', [Validators.maxLength(10), CustomValidators.zip]),
      isDme: new FormControl(false),
      therapy: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      otherProduct: new FormControl('', Validators.maxLength(100)),
      insurance: new FormControl('', [Validators.required, Validators.maxLength(100)])
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit() {

    if (!this.form.valid) {
      return;
    }

    const patient = this.buildPatient();

    this.patientApi
      .post(patient)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newPatient: Patient) => {
        this.router.navigate(['agent/patient/', newPatient.patientId, 'intake-form']);
      });
  }

  openDmeDialog(): void {
    const dialogRef = this.dialog.open(DmaDialogComponent);
  }

  private buildPatient(): Patient {

    const patient = new Patient();
    patient.agentId = this.agentId;
    patient.firstName = this.form.controls['firstName'].value;
    patient.middleName = this.form.controls['middleName'].value;
    patient.lastName = this.form.controls['lastName'].value;
    patient.dateOfBirth = this.form.controls['dateOfBirth'].value;
    patient.phoneNumber = this.form.controls['phoneNumber'].value;
    patient.language = this.form.controls['language'].value;
    patient.callBackImmediately = this.form.controls['callBackImmediately'].value;
    patient.bestTimeToCallBack = this.form.controls['bestTimeToCallBack'].value;

    patient.medications = this.form.controls['medications'].value;
    patient.notes = this.form.controls['notes'].value;
    patient.otherProducts = this.form.controls['otherProduct'].value;
    patient.physiciansName = this.form.controls['physicianName'].value;
    patient.physiciansPhoneNumber = this.form.controls['physicianPhoneNumber'].value;
    patient.therapy = this.form.controls['therapy'].value;
    patient.insurance = this.form.controls['insurance'].value;
    patient.pharmacy = this.form.controls['pharmacy'].value;
    patient.isDme = this.form.controls['isDme'].value;

    patient.address = new Address();
    patient.address.addressLineOne = this.form.controls['addressLineOne'].value;
    patient.address.addressLineTwo = this.form.controls['addressLineTwo'].value;
    patient.address.city = this.form.controls['city'].value;
    patient.address.state = this.form.controls['state'].value;
    patient.address.zipCode = this.form.controls['zip'].value;


    patient.physiciansAddress = new Address();
    patient.physiciansAddress.addressLineOne = this.form.controls['physicianAddressLineOne'].value;
    patient.physiciansAddress.addressLineTwo = this.form.controls['physicianAddressLineTwo'].value;
    patient.physiciansAddress.city = this.form.controls['physicianCity'].value;
    patient.physiciansAddress.state = this.form.controls['physicianState'].value;
    patient.physiciansAddress.zipCode = this.form.controls['physicianZip'].value;

    if (patient.physiciansAddress.addressLineOne.length == 0 
      || patient.physiciansAddress.zipCode.length == 0
      || patient.physiciansAddress.city.length == 0
      || patient.physiciansAddress.state.length == 0) {
      patient.physiciansAddress = null;
    }

    patient.phoneNumber = patient.phoneNumber.replace("(", "").replace(")", "").replace(" ","").replace("-", "");
    return patient;
  }

}
