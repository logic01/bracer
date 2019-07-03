import { Component, OnInit, ViewChild } from '@angular/core';
import { MatCheckboxChange, MatSort, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';

import { forkJoin, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';

import { RouteUrls } from 'src/app/constants/routes';
import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-billing-dashboard',
  templateUrl: './billing-dashboard.component.html',
  styleUrls: ['./billing-dashboard.component.scss']
})
export class BillingDashboardComponent implements OnInit {

  @ViewChild(MatSort) sort: MatSort;

  // for the UI to access Enum
  public IntakeStatus = IntakeStatus;

  private changes: IntakeForm[] = [];
  private unsubscribe$ = new Subject();
  public dataSource: MatTableDataSource<IntakeForm>;
  public columnsToDisplay = ['intakeFormId', 'createdOn', 'status', 'physicianPaid', 'vendorBilled', 'vendorPaid', 'download'];

  constructor(
    private readonly router: Router,
    private readonly intakeFormApi: IntakeFormService) { }

  ngOnInit() {

    this.intakeFormApi.getAll()
      .pipe(
        map(intakes => intakes.filter(v => v.status >= IntakeStatus.Approved)),
        takeUntil(this.unsubscribe$))
      .subscribe((intakeFormList: IntakeForm[]) => {
        this.dataSource = new MatTableDataSource(intakeFormList);
        this.dataSource.sort = this.sort;
      });

  }

  vendorPaid(event: MatCheckboxChange, intakeFormId: string) {

    const alreadyChangedItem = this.changes.find((item: IntakeForm) => item.intakeFormId === intakeFormId);

    if (!alreadyChangedItem) {
      const newIttem = this.dataSource.data.find((item: IntakeForm) => item.intakeFormId === intakeFormId);
      newIttem.vendorPaid = event.checked;
      this.changes.push(newIttem);
    } else {
      alreadyChangedItem.vendorPaid = event.checked;
      const index = this.changes.indexOf(alreadyChangedItem);
      this.changes[index] = alreadyChangedItem;
    }

  }


  vendorBilled(event: MatCheckboxChange, intakeFormId: string) {

    const alreadyChangedItem = this.changes.find((item: IntakeForm) => item.intakeFormId === intakeFormId);

    if (!alreadyChangedItem) {
      const newIttem = this.dataSource.data.find((item: IntakeForm) => item.intakeFormId === intakeFormId);
      newIttem.vendorBilled = event.checked;
      this.changes.push(newIttem);
    } else {
      alreadyChangedItem.vendorBilled = event.checked;
      const index = this.changes.indexOf(alreadyChangedItem);
      this.changes[index] = alreadyChangedItem;
    }

  }

  physicianPaid(event: MatCheckboxChange, intakeFormId: string) {
    const alreadyChangedItem = this.changes.find((item: IntakeForm) => item.intakeFormId === intakeFormId);

    if (!alreadyChangedItem) {
      const newIttem = this.dataSource.data.find((item: IntakeForm) => item.intakeFormId === intakeFormId);
      newIttem.physicianPaid = event.checked;
      this.changes.push(newIttem);
    } else {
      alreadyChangedItem.physicianPaid = event.checked;
      const index = this.changes.indexOf(alreadyChangedItem);
      this.changes[index] = alreadyChangedItem;
    }

  }


  save() {

    const observables = [];

    this.changes.forEach((intake: IntakeForm) => observables.push(this.intakeFormApi.put(intake.intakeFormId, intake)));

    forkJoin(observables).subscribe(() => this.router.navigateByUrl(RouteUrls.AdminDashboardComponent));

  }

  download(documentId: string) {
    window.location.href = `${environment.api_url}/document/${documentId}/download`;
  }
}
