import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from 'src/app/constants/routes';
import { Vendor } from 'src/app/models/vendor.model';
import { VendorService } from 'src/app/services/api/vendor.service';

@Component({
  selector: 'app-edit-vendor',
  templateUrl: './edit-vendor.component.html',
  styleUrls: ['./edit-vendor.component.scss']
})
export class EditVendorComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();
  public vendor$: Observable<Vendor>;

  constructor(
    private readonly vendorApi: VendorService,
    private readonly route: ActivatedRoute,
    private readonly router: Router) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.vendor$ = this.vendorApi.get(id);
  }

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
