import { Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatRadioChange, MatRadioGroup, MatSelect } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { Address } from 'src/app/models/address.model';
import { CallbackTime } from 'src/app/models/enums/callback-time.enum';
import { InsuranceType } from 'src/app/models/enums/insurance-type.enum';
import { LanguageType } from 'src/app/models/enums/language-type.enum';
import { PharmacyType } from 'src/app/models/enums/pharmacy-type.enum';
import { SexType } from 'src/app/models/enums/sex-type.enum';
import { TherapyType } from 'src/app/models/enums/therapy-type.enum';
import { Medicare } from 'src/app/models/medicare.model';
import { Patient } from 'src/app/models/patient.model';
import { PrivateInsurance } from 'src/app/models/private-insurance.model';
import { FormatHelperService } from 'src/app/services/format-helper.service';
import { MaskService } from 'src/app/services/mask.service';
import { SelectValueService } from 'src/app/services/select-value.service';
import { SessionService } from 'src/app/services/session.service';
import { CustomValidators } from 'src/app/validators/custom-validators';

import { DmaDialogComponent } from '../dma-dialog/dma-dialog.component';

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html',
  styleUrls: ['./patient-form.component.scss']
})
export class PatientFormComponent implements OnInit, OnDestroy {

  @Input() patient$: Observable<Patient>;
  @Output() formSubmitEvent = new EventEmitter<Patient>();

  @ViewChild('genderC') genderField: MatRadioGroup;
  @ViewChild('firstC') firstNameField: ElementRef;
  @ViewChild('lastC') lastNameField: ElementRef;
  @ViewChild('dobC') dobField: ElementRef;
  @ViewChild('phoneC') phoneField: ElementRef;
  @ViewChild('heightC') heightField: MatSelect;
  @ViewChild('weightC') weightField: ElementRef;
  @ViewChild('waistC') waistField: ElementRef;
  @ViewChild('shoeC') shoeField: MatSelect;
  @ViewChild('addressC') addressField: ElementRef;
  @ViewChild('cityC') cityField: ElementRef;
  @ViewChild('stateC') stateField: ElementRef;
  @ViewChild('zipC') zipField: ElementRef;
  @ViewChild('callbackC') callbackField: MatSelect;
  @ViewChild('pharmacyC') pharmacyField: MatSelect;
  @ViewChild('therapyC') therapyField: MatSelect;
  @ViewChild('insuranceC') insuranceField: MatRadioGroup;

  private unsubscribe$ = new Subject();
  private agentId: string;

  public form: FormGroup;
  public shoeSizes: string[] = SelectValueService.shoeSizes;
  public heights: string[] = SelectValueService.heights;
  public submitted = false;
  public privateInsurance: boolean;
  public medicareInsurance: boolean;

  constructor(
    private readonly session: SessionService,
    private readonly dialog: MatDialog,
    private readonly route: ActivatedRoute,
    public readonly maskService: MaskService,
    public readonly formatHelper: FormatHelperService) { }

  ngOnInit() {

    this.agentId = this.route.snapshot.paramMap.get('agentId');

    this.form = new FormGroup({
      firstName: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      middleName: new FormControl('', Validators.maxLength(100)),
      lastName: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      dateOfBirth: new FormControl('', [Validators.required]),
      phoneNumber: new FormControl('', [Validators.required, CustomValidators.phonenumber]),
      height: new FormControl('', [Validators.required, Validators.maxLength(6)]),
      weight: new FormControl('', [Validators.required, CustomValidators.onlyNumeric, Validators.maxLength(3)]),
      waist: new FormControl('', [Validators.required, CustomValidators.onlyNumeric, Validators.maxLength(3)]),
      shoeSize: new FormControl('', [Validators.required, Validators.maxLength(4)]),
      addressLineOne: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      addressLineTwo: new FormControl('', Validators.maxLength(100)),
      city: new FormControl('', [Validators.required, Validators.maxLength(30)]),
      state: new FormControl('', Validators.required),
      zipCode: new FormControl('', [Validators.required, Validators.maxLength(10), CustomValidators.zip]),
      sex: new FormControl('Male', Validators.required),
      language: new FormControl('English', Validators.required),
      callBackImmediately: new FormControl(false, Validators.required),
      bestTimeToCallBack: new FormControl('', Validators.required),
      allergies: new FormControl('', [Validators.maxLength(500)]),

      pharmacy: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      medications: new FormControl('', Validators.maxLength(100)),
      notes: new FormControl('', Validators.maxLength(100)),
      physiciansName: new FormControl('', Validators.maxLength(100)),
      physiciansPhoneNumber: new FormControl('', CustomValidators.phonenumber),
      physicianAddressLineOne: new FormControl('', Validators.maxLength(100)),
      physicianAddressLineTwo: new FormControl('', Validators.maxLength(100)),
      physicianCity: new FormControl('', Validators.maxLength(30)),
      physicianState: new FormControl(''),
      physicianZip: new FormControl('', [Validators.maxLength(10), CustomValidators.zip]),
      isDme: new FormControl(false),
      therapy: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      otherProducts: new FormControl('', Validators.maxLength(100)),
      insurance: new FormControl('', Validators.maxLength(100)),
      insuranceId: new FormControl('', Validators.maxLength(100)),
      privateGroup: new FormControl('', Validators.maxLength(100)),
      privatePcn: new FormControl('', Validators.maxLength(100)),
      bin: new FormControl('', [Validators.maxLength(6), CustomValidators.onlyNumeric]),
      insuranceStreet: new FormControl('', Validators.maxLength(100)),
      insuranceCity: new FormControl('', Validators.maxLength(30)),
      insuranceState: new FormControl('', Validators.maxLength(100)),
      insuranceZip: new FormControl('', CustomValidators.zip),
      insurancePhone: new FormControl('', CustomValidators.phonenumber),
      memberId: new FormControl('', Validators.maxLength(100)),
      medicareGroup: new FormControl('', Validators.maxLength(100)),
      medicarePcn: new FormControl('', Validators.maxLength(100)),
      subscriberNumber: new FormControl('', Validators.maxLength(100)),
      secondaryInsurance: new FormControl('', Validators.maxLength(100)),
      secondarySubscriberNumber: new FormControl('', Validators.maxLength(100)),
      insuranceType: new FormControl('', Validators.required)
    });

    // populate form if we have a vendor bound to the form
    if (this.patient$) {
      this.patient$.subscribe((result: Patient) => {
        this.form.patchValue(result);
        this.form.patchValue(result.address);
        this.form.patchValue(
          {
            physicianAddressLineOne: result.address.addressLineOne,
            physicianAddressLineTwo: result.address.addressLineTwo,
            physicianCity: result.address.city,
            physicianState: result.address.state,
            physicianZip: result.address.zipCode,
          });
        this.form.patchValue(
          {
            isDme: result.isDme ? 'true' : 'false',
            pharmacy: PharmacyType[result.pharmacy],
            sex: SexType[result.sex],
            language: LanguageType[result.language],
            therapy: TherapyType[result.therapy],
            insuranceType: InsuranceType[result.insurance],
            bestTimeToCallBack: CallbackTime[result.bestTimeToCallBack]
          });


      });
    }
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit() {

    this.submitted = true;

    if (!this.form.valid) {
      this.focusOnErrorElement();
      return;
    }

    const patient = this.buildPatient();

    this.formSubmitEvent.emit(patient);
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
    patient.phoneNumber = this.formatHelper.toNumbersOnly(this.form.controls['phoneNumber'].value);
    patient.allergies = this.form.controls['allergies'].value;
    patient.waist = this.form.controls['waist'].value;
    patient.weight = this.form.controls['weight'].value;
    patient.shoeSize = this.form.controls['shoeSize'].value;
    patient.height = this.form.controls['height'].value;

    patient.language = this.form.controls['language'].value;
    patient.callBackImmediately = this.form.controls['callBackImmediately'].value;
    patient.bestTimeToCallBack = this.form.controls['bestTimeToCallBack'].value;

    patient.medications = this.form.controls['medications'].value;
    patient.notes = this.form.controls['notes'].value;
    patient.otherProducts = this.form.controls['otherProducts'].value;
    patient.physiciansName = this.form.controls['physiciansName'].value;
    patient.physiciansPhoneNumber = this.formatHelper.toNumbersOnly(this.form.controls['physiciansPhoneNumber'].value);

    patient.therapy = this.form.controls['therapy'].value;
    patient.insurance = this.form.controls['insuranceType'].value;
    patient.pharmacy = this.form.controls['pharmacy'].value;
    patient.isDme = this.form.controls['isDme'].value;

    patient.address = new Address();
    patient.address.addressLineOne = this.form.controls['addressLineOne'].value;
    patient.address.addressLineTwo = this.form.controls['addressLineTwo'].value;
    patient.address.city = this.form.controls['city'].value;
    patient.address.state = this.form.controls['state'].value;
    patient.address.zipCode = this.form.controls['zipCode'].value;

    patient.physiciansAddress = new Address();
    patient.physiciansAddress.addressLineOne = this.form.controls['physicianAddressLineOne'].value;
    patient.physiciansAddress.addressLineTwo = this.form.controls['physicianAddressLineTwo'].value;
    patient.physiciansAddress.city = this.form.controls['physicianCity'].value;
    patient.physiciansAddress.state = this.form.controls['physicianState'].value;
    patient.physiciansAddress.zipCode = this.form.controls['physicianZip'].value;

    if (this.privateInsurance) {
      patient.privateInsurance = new PrivateInsurance();
      patient.privateInsurance.bin = this.form.controls['bin'].value;
      patient.privateInsurance.insurance = this.form.controls['insurance'].value;
      patient.privateInsurance.insuranceId = this.form.controls['insuranceId'].value;
      patient.privateInsurance.group = this.form.controls['privateGroup'].value;
      patient.privateInsurance.pcn = this.form.controls['privatePcn'].value;
      patient.privateInsurance.street = this.form.controls['insuranceStreet'].value;
      patient.privateInsurance.city = this.form.controls['insuranceCity'].value;
      patient.privateInsurance.state = this.form.controls['insuranceState'].value;
      patient.privateInsurance.zip = this.form.controls['insuranceZip'].value;
      patient.privateInsurance.phone = this.formatHelper.toNumbersOnly(this.form.controls['insurancePhone'].value);
    }

    if (this.medicareInsurance) {
      patient.medicare = new Medicare();
      patient.medicare.memberId = this.form.controls['memberId'].value;
      patient.medicare.patientGroup = this.form.controls['medicareGroup'].value;
      patient.medicare.pcn = this.form.controls['medicarePcn'].value;
      patient.medicare.subscriberNumber = this.form.controls['subscriberNumber'].value;
      patient.medicare.secondaryCarrier = this.form.controls['secondaryInsurance'].value;
      patient.medicare.secondarySubscriberNumber = this.form.controls['secondarySubscriberNumber'].value;
    }

    if (patient.physiciansAddress.addressLineOne.length === 0
      || patient.physiciansAddress.zipCode.length === 0
      || patient.physiciansAddress.city.length === 0
      || patient.physiciansAddress.state.length === 0) {
      patient.physiciansAddress = null;
    }

    return patient;
  }

  radioChange(event: MatRadioChange) {
    this.privateInsurance = event.value === 'PRIVATE';
    this.medicareInsurance = event.value === 'MEDICARE';
  }

  focusOnErrorElement() {
    // tslint:disable-next-line: forin
    for (const field in this.form.controls) {
      const control: AbstractControl = this.form.get(field);

      if (control.errors != null) {
        switch (field) {
          case 'gender':
            this.genderField._radios[0].focus();
            return;
          case 'firstName':
            this.firstNameField.nativeElement.focus();
            return;
          case 'lastName':
            this.lastNameField.nativeElement.focus();
            return;
          case 'dateOfBirth':
            this.dobField.nativeElement.focus();
            return;
          case 'phoneNumber':
            this.phoneField.nativeElement.focus();
            return;
          case 'height':
            this.heightField.focus();
            return;
          case 'weight':
            this.weightField.nativeElement.focus();
            return;
          case 'waist':
            this.waistField.nativeElement.focus();
            return;
          case 'shoes':
            this.shoeField.focus();
            return;
          case 'addressLineOne':
            this.addressField.nativeElement.focus();
            return;
          case 'city':
            this.cityField.nativeElement.focus();
            return;
          case 'state':
            this.stateField.nativeElement.focus();
            return;
          case 'zip':
            this.zipField.nativeElement.focus();
            return;
          case 'bestTimeToCallBack':
            this.callbackField.focus();
            return;
          case 'pharmacy':
            this.pharmacyField.focus();
            return;
          case 'therapy':
            this.therapyField.focus();
            return;
          case 'insuranceType':
            this.insuranceField._radios[0].focus();
            return;

        }
      }
    }
  }

}
