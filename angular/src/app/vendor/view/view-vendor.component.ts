import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatTable } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Physician } from 'src/app/models/physician.model';
import { Vendor } from 'src/app/models/vendor.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { PhysicianService } from 'src/app/services/api/physician.service';
import { VendorService } from 'src/app/services/api/vendor.service';

import { AssignmentDialogComponent } from '../assignment-dialog/assignment-dialog.component';

export class TableRow {
  intakeFormId: string;
  physicianName: string;
  physicianState: string;
  status: IntakeStatus;
}

@Component({
  selector: 'app-view-vendor',
  templateUrl: './view-vendor.component.html',
  styleUrls: ['./view-vendor.component.scss']
})
export class ViewVendorComponent implements OnInit, OnDestroy {

  @ViewChild(MatTable) table: MatTable<TableRow>;

  public vendor$: Observable<Vendor>;
  public physicians$: Observable<Physician[]>;
  public intake$: Observable<IntakeForm[]>;

  public data: TableRow[] = [];
  public columnsToDisplay = ['intakeFormId', 'status', 'physicianName', 'physicianState', 'actions'];

  private unsubscribe$ = new Subject();

  constructor(
    private readonly router: Router,
    private readonly physicianApi: PhysicianService,
    private readonly vendorApi: VendorService,
    private readonly intakeApi: IntakeFormService,
    private readonly route: ActivatedRoute,
    private readonly dialog: MatDialog) {
  }

  ngOnInit() {

    const vendorId = this.route.snapshot.paramMap.get('id');

    this.vendor$ = this.vendorApi.get(vendorId);
    this.physicians$ = this.physicianApi.getAll();
    this.intake$ = this.intakeApi.getByVendor(vendorId);

    forkJoin([this.intake$, this.physicians$])
      .pipe(
        takeUntil(this.unsubscribe$),
        map(responses => {

          const intakeForms = responses[0];
          const physicians = responses[1];

          intakeForms.forEach((intake: IntakeForm) => {

            const row = new TableRow();
            row.intakeFormId = intake.intakeFormId;
            row.status = intake.status;
            row.physicianName = this.getPhysicianName(intake.physicianId, physicians);
            row.physicianState = this.getPhysicianState(intake.physicianId, physicians);
            this.data.push(row);
          });

          this.table.renderRows();
        }))
      .subscribe();

  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  getPhysicianName(physicianId: string, physicians: Physician[]): string {

    const physician = physicians.find(x => x.userAccount.userAccountId === physicianId);

    if (physician) {
      return `${physician.firstName} ${physician.lastName}`;
    }

    return '';
  }

  getPhysicianState(physicianId: string, physicians: Physician[]): string {

    const physician = physicians.find(x => x.userAccount.userAccountId === physicianId);

    if (physician) {
      return physician.address.state;
    }

    return '';
  }

  assign(id: string): void {
    const dialogRef = this.dialog.open(AssignmentDialogComponent, { data: { intakeFormId: id } });
  }

  view(id: string) {
    this.router.navigate(['intake-document/', id]);
  }

  email(id: string): void {
    const dialogRef = this.dialog.open(AssignmentDialogComponent, { data: { intakeFormId: id } });
  }

}
