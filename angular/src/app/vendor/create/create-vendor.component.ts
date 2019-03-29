import { Component, OnInit, OnDestroy } from '@angular/core';
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
export class CreateVendorComponent implements OnDestroy {

  private unsubscribe$ = new Subject();

  constructor(
    private readonly vendorApi: VendorService,
    private readonly router: Router) { }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit(vendor: Vendor) {

    this.vendorApi
      .post(vendor)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newVendor: Vendor) => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });
  }

}
