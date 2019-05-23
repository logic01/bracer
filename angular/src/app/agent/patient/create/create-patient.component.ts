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

@Component({
  selector: 'app-create-patient',
  templateUrl: './create-patient.component.html',
  styleUrls: ['./create-patient.component.scss']
})
export class CreatePatientComponent implements OnInit, OnDestroy {

  form: FormGroup;
  private unsubscribe$ = new Subject();
  private agentId: string;

  constructor(
    private readonly session: SessionService,
    private readonly dialog: MatDialog,
    private readonly patientApi: PatientService,
    private readonly router: Router) { }

  ngOnInit() {

    /* only an agent can access this page */
    this.session.userAccount$.subscribe((result: UserAccount) => {
      this.agentId = result.userAccountId;
    });

    this.form = new FormGroup({
      firstName: new FormControl('', Validators.required),
      middleName: new FormControl(''),
      lastName: new FormControl('', Validators.required),
      dateOfBirth: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
      addressLineOne: new FormControl('', Validators.required),
      addressLineTwo: new FormControl(''),
      city: new FormControl('', Validators.required),
      state: new FormControl('', Validators.required),
      zip: new FormControl('', Validators.required),
      sex: new FormControl('', Validators.required),
      language: new FormControl('English', Validators.required),
      callBackImmediately: new FormControl(false, Validators.required),
      bestTimeToCallBack: new FormControl('', Validators.required),

      pharmacy: new FormControl('', Validators.required),
      medications: new FormControl(''),
      notes: new FormControl(''),
      physicianName: new FormControl('', Validators.required),
      physicianPhoneNumber: new FormControl('', Validators.required),
      physicianAddressLineOne: new FormControl('', Validators.required),
      physicianAddressLineTwo: new FormControl(''),
      physicianCity: new FormControl('', Validators.required),
      physicianState: new FormControl('', Validators.required),
      physicianZip: new FormControl('', Validators.required),
      isDme: new FormControl(false),
      therapy: new FormControl('', Validators.required),
      otherProduct: new FormControl(''),
      insurance: new FormControl('', Validators.required)
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

    return patient;
  }

}
