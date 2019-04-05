import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material';

import { DmaDialogComponent } from '../dma-dialog/dma-dialog.component';

@Component({
  selector: 'app-medical-information',
  templateUrl: './medical-information.component.html',
  styleUrls: ['./medical-information.component.scss']
})
export class MedicalInformationComponent implements OnInit {

  form: FormGroup;

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
    this.form = new FormGroup({
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
      isDma: new FormControl('', Validators.required),
      therapyType: new FormControl('', Validators.required),
      otherProduct: new FormControl(''),
      insuranceType: new FormControl('', Validators.required)
    });

  }

  onSubmit() {

  }

  openDmaDialog(): void {
    const dialogRef = this.dialog.open(DmaDialogComponent);
  }
}
