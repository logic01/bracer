import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';

import { Observable, Subject } from 'rxjs';

import { IntakeForm } from 'src/app/models/intake-form.model';
import { Patient } from 'src/app/models/patient.model';
import { Physician } from 'src/app/models/physician.model';
import { Vendor } from 'src/app/models/vendor.model';
import { TableRow } from 'src/app/vendor/view/view-vendor.component';

@Component({
  selector: 'app-vendor-report',
  templateUrl: './vendor-report.component.html',
  styleUrls: ['./vendor-report.component.scss']
})
export class VendorReportComponent implements OnInit, OnDestroy {

  @ViewChild(MatSort) sort: MatSort;

  public datasource: MatTableDataSource<TableRow>;

  public vendor$: Observable<Vendor>;
  public physicians$: Observable<Physician[]>;
  public intake$: Observable<IntakeForm[]>;
  public patient$: Observable<Patient[]>;

  public columnsToDisplay = ['intakeFormId', 'status', 'physicianName', 'physicianState', 'actions'];

  private vendorId: string;
  private unsubscribe$ = new Subject();

  constructor() {
  }


  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }
}
