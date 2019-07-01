import { Component, OnInit, ViewChild } from '@angular/core';
import { MatCheckboxChange, MatSort, MatTableDataSource } from '@angular/material';

import { Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';

import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-billing-dashboard',
  templateUrl: './billing-dashboard.component.html',
  styleUrls: ['./billing-dashboard.component.scss']
})
export class BillingDashboardComponent implements OnInit {

  @ViewChild(MatSort) sort: MatSort;

  private changes: IntakeForm[] = [];
  private unsubscribe$ = new Subject();
  public dataSource: MatTableDataSource<IntakeForm>;
  public columnsToDisplay = ['intakeFormId', 'createdOn', 'status', 'physicianPaid', 'vendorBilled', 'vendorPaid'];

  constructor(
    private readonly session: SessionService,
    private readonly intakeFormApi: IntakeFormService) { }

  ngOnInit() {

    this.session.userAccount$.subscribe((account: UserAccount) => {

      this.intakeFormApi.getAll()
        .pipe(
          map(intakes => intakes.filter(v => v.status === IntakeStatus.Downloaded)),
          takeUntil(this.unsubscribe$))
        .subscribe((intakeFormList: IntakeForm[]) => {
          this.dataSource = new MatTableDataSource(intakeFormList);
          this.dataSource.sort = this.sort;
        });

    });

  }

  vendorPaid(event: MatCheckboxChange, intakeFormId: string) {

    let updateItem = this.changes.find(this.findIndexToUpdate, intakeFormId);

    if (!updateItem) {
      updateItem = this.dataSource.data.find(this.findIndexToUpdate, intakeFormId);
    }

    const index = this.changes.indexOf(updateItem);

    updateItem.vendorPaid = event.checked;

    this.changes[index] = updateItem;

  }


  vendorBilled(event: MatCheckboxChange, intakeFormId: string) {

    let updateItem = this.changes.find(this.findIndexToUpdate, intakeFormId);

    if (!updateItem) {
      updateItem = this.dataSource.data.find(this.findIndexToUpdate, intakeFormId);
    }

    const index = this.changes.indexOf(updateItem);

    updateItem.vendorBilled = event.checked;

    this.changes[index] = updateItem;

  }

  physicianPaid(event: MatCheckboxChange, intakeFormId: string) {
    let updateItem = this.changes.find(this.findIndexToUpdate, intakeFormId);

    if (!updateItem) {
      updateItem = this.dataSource.data.find(this.findIndexToUpdate, intakeFormId);
    }

    const index = this.changes.indexOf(updateItem);

    updateItem.physicianPaid = event.checked;

    this.changes[index] = updateItem;
  }

  private findIndexToUpdate(newItem) {
    return newItem.id === this;
  }

  save() {



  }
}
