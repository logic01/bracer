import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material';

import { DmaDialogComponent } from '../dma-dialog/dma-dialog.component';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  public form: FormGroup;

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
    this.form = new FormGroup({
      language: new FormControl('English', Validators.required),
      callBackImmediately: new FormControl(),
      bestTimeToCallBack: new FormControl('', Validators.required),
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
      isDma: new FormControl('', Validators.required)
    });
  }

  openDmaDialog(): void {
    const dialogRef = this.dialog.open(DmaDialogComponent, {
      data: 'dave'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      const res = result;
    });

  }
}
