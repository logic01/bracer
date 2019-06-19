import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
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
  public physicians$: Observable<Physician[]>;
  public intake$: Observable<IntakeForm[]>;
  public patient$: Observable<Patient[]>;

  public columnsToDisplay = ['intakeFormId', 'status', 'physicianName', 'physicianState', 'actions'];

  private vendorId: string;
  private unsubscribe$ = new Subject();

  constructor(
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
    this.physicians$ = this.physicianApi.getAll();
    this.intake$ = this.intakeApi.getByVendor(this.vendorId);
  //  this.patient$ = this.patientApi.getByVendor(this.vendorId);
// , this.patient$
    forkJoin([this.intake$, this.physicians$])
      .pipe(
        takeUntil(this.unsubscribe$),
        map(responses => {

          const intakeForms = responses[0];
          const physicians = responses[1];
        //  const patients = responses[2];

          const data: TableRow[] = [];
          intakeForms.forEach((intake: IntakeForm) => {

            const row = new TableRow();
            row.intakeFormId = intake.intakeFormId;
            row.status = intake.status;
            row.physicianName = this.getPhysicianName(intake.physicianId, physicians);
            row.physicianState = this.getPhysicianState(intake.physicianId, physicians);
          //  row.patientName = this.getPatientName(intake.patientId, patients);
          //  row.patientState = this.getPatientState(intake.patientId, patients);
            data.push(row);
          });

          this.datasource = new MatTableDataSource(data);
          this.datasource.sort = this.sort;
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

  getPatientName(patientId: string, patients: Patient[]): string {

    const patient = patients.find(x => x.patientId === patientId);

    if (patient) {
      return `${patient.firstName} ${patient.lastName}`;
    }

    return '';
  }

  getPatientState(patientId: string, patients: Patient[]): string {

    const patient = patients.find(x => x.patientId === patientId);

    if (patient) {
      return patient.address.state;
    }

    return '';
  }

  assign(intakeFormId: string): void {
    const dialogRef = this.dialog.open(AssignmentDialogComponent, { data: { intakeFormId: intakeFormId } });
  }

  view(intakeFormId: string) {
    this.router.navigate(['vendor', this.vendorId, 'intake-document', intakeFormId]);
  }

  email(documentId: string): void {
    const dialogRef = this.dialog.open(AssignmentDialogComponent, { data: { intakeFormId: documentId } });
  }

}
