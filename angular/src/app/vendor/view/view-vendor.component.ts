import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Document } from '../../models/document.model';
import { Vendor } from 'src/app/models/vendor.model';
import { DocumentService } from 'src/app/services/api/document.service';
import { VendorService } from 'src/app/services/api/vendor.service';

@Component({
  selector: 'app-view-vendor',
  templateUrl: './view-vendor.component.html',
  styleUrls: ['./view-vendor.component.scss']
})
export class ViewVendorComponent implements OnInit {

  public vendor$: Observable<Vendor>;

  columnsToDisplay = ['billed', 'documentId', 'type', 'status', 'sign'];

  data: Document[];

  private unsubscribe$ = new Subject();

  constructor(
    private readonly vendorApi: VendorService,
    private readonly documentApi: DocumentService,
    private readonly route: ActivatedRoute) {
  }

  ngOnInit() {
    const vendorId = this.route.snapshot.paramMap.get('id');
    this.vendor$ = this.vendorApi.get(vendorId);

    this.documentApi
      .getByVendor(vendorId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((documentList: Document[]) => {
        this.data = documentList;
      });

  }

}
