import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, Observable, Subject } from 'rxjs';
import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Patient } from 'src/app/models/patient.model';
import { Physician } from 'src/app/models/physician.model';
import { Vendor } from 'src/app/models/vendor.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { PatientService } from 'src/app/services/api/patient.service';
import { PhysicianService } from 'src/app/services/api/physician.service';
import { VendorService } from 'src/app/services/api/vendor.service';

import { AssignmentDialogComponent } from '../assignment-dialog/assignment-dialog.component';

export class TableRow {
  intakeFormId: string;
  physicianName: string;
  physicianState: string;
  patientName: string;
  patientState: string;
  status: IntakeStatus;
}

@Component({
  selector: 'app-view-vendor',
  templateUrl: './view-vendor.component.html',
  styleUrls: ['./view-vendor.component.scss']
})
export class ViewVendorComponent implements OnInit, OnDestroy {

  @ViewChild(MatSort) sort: MatSort;

  public datasource: MatTableDataSource<TableRow>;

  public vendor$: Observable<Vendor>;

  public columnsToDisplay = ['intakeFormId', 'status', 'physicianName', 'physicianState', 'patientName', 'patientState', 'actions'];

  private vendorId: string;
  private data: TableRow[] = [];
  private unsubscribe$ = new Subject();

  constructor(
    private readonly changeDetectorRefs: ChangeDetectorRef,
    private readonly router: Router,
    private readonly physicianApi: PhysicianService,
    private readonly vendorApi: VendorService,
    private readonly patientApi: PatientService,
    private readonly intakeApi: IntakeFormService,
    private readonly route: ActivatedRoute,
    private readonly dialog: MatDialog) {
  }

  ngOnInit() {

    this.vendorId = this.route.snapshot.paramMap.get('id');

    this.vendor$ = this.vendorApi.get(this.vendorId);

    this.datasource = new MatTableDataSource(this.data);
    this.datasource.sort = this.sort;

    this.intakeApi.getByVendor(this.vendorId).subscribe((intakes: IntakeForm[]) => {

      intakes.forEach((intake: IntakeForm) => {

        if (intake.patientId && intake.physicianId) {
          this.buildBothRow(intake);
        } else if (intake.patientId) {
          this.buildPatientOnlyRow(intake);
        } else if (intake.physicianId) {
          this.buildPhysicianOnlyRow(intake);
        }

      });

    });

  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  buildBothRow(intake: IntakeForm) {
    const physician$ = this.physicianApi.get(intake.physicianId);
    const patient$ = this.patientApi.get(intake.patientId);

    forkJoin([physician$, patient$]).subscribe(responses => {

      const physician = responses[0];
      const patient = responses[1];

      const row = this.buildTableRow(intake, physician, patient);
      this.data.push(row);

      this.datasource = new MatTableDataSource(this.data);
      this.datasource.sort = this.sort;
    });
  }

  buildPhysicianOnlyRow(intake: IntakeForm) {
    this.physicianApi
      .get(intake.physicianId)
      .subscribe((physician: Physician) => {

        const row = this.buildTableRow(intake, physician, undefined);
        this.data.push(row);

        this.datasource = new MatTableDataSource(this.data);
        this.datasource.sort = this.sort;
      });
  }

  buildPatientOnlyRow(intake: IntakeForm) {
    this.patientApi
      .get(intake.patientId)
      .subscribe((patient: Patient) => {

        const row = this.buildTableRow(intake, undefined, patient);
        this.data.push(row);

        this.datasource = new MatTableDataSource(this.data);
        this.datasource.sort = this.sort;
      });
  }


  buildTableRow(intake: IntakeForm, physician?: Physician, patient?: Patient): TableRow {
    const row = new TableRow();
    row.intakeFormId = intake.intakeFormId;
    row.status = intake.status;

    if (patient) {
      row.patientName = patient.firstName + ' ' + patient.lastName;
      row.patientState = patient.address.state;
    }

    if (physician) {
      row.physicianName = physician.firstName + ' ' + physician.lastName;
      row.physicianState = physician.address.state;
    }

    return row;
  }

  assign(intakeFormId: string): void {
    const dialogRef = this.dialog.open(AssignmentDialogComponent, { data: { intakeFormId: intakeFormId } });
  }

  view(intakeFormId: string) {
    this.router.navigate(['vendor', this.vendorId, 'intake-document', intakeFormId]);
  }

}
