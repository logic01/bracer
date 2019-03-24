import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { RouteUrls } from 'src/app/constants/routes';
import { Subject } from 'rxjs';
import { VendorService } from 'src/app/api/vendor.service';
import { takeUntil } from 'rxjs/operators';
import { Vendor } from 'src/app/models/vendor.model';

@Component({
  selector: 'app-create-vendor',
  templateUrl: './create-vendor.component.html',
  styleUrls: ['./create-vendor.component.scss']
})
export class CreateVendorComponent implements OnInit, OnDestroy {

  public accountForm: FormGroup;
  private unsubscribe$ = new Subject();

  constructor(
    private readonly vendorApi: VendorService,
    private readonly router: Router) { }

  ngOnInit() {
    this.accountForm = new FormGroup({
      companyName: new FormControl('', Validators.required),
      doingBusinessAs: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
      contactFirstName: new FormControl('', Validators.required),
      contactLastName: new FormControl('', Validators.required),
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit() {

    if (!this.accountForm.valid) {
      return;
    }

    const vendor = this.buildVendor();

    this.vendorApi
      .post(vendor)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newVendor: Vendor) => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });
  }

  private buildVendor(): Vendor {

    const vendor = new Vendor();

    vendor.companyName = this.accountForm.controls['companyName'].value;
    vendor.doingBusinessAs = this.accountForm.controls['doingBusinessAs'].value;
    vendor.phoneNumber = this.accountForm.controls['phoneNumber'].value;
    vendor.contactFirstName = this.accountForm.controls['contactFirstName'].value;
    vendor.contactLastName = this.accountForm.controls['contactLastName'].value;

    return vendor;
  }
}
