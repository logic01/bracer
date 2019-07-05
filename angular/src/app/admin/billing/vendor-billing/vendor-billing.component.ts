import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';

import { Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';

import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Patient } from 'src/app/models/patient.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { PatientService } from 'src/app/services/api/patient.service';


export class TableRow {
  intakeFormId: string;
  createdOn: string;
  status: IntakeStatus;
  patientName: string;
  patientState: string;
  vendorName: string;
  vendorBilled: boolean;
  vendorPaid: boolean;
}


@Component({
  selector: 'app-vendor-billing',
  templateUrl: './vendor-billing.component.html',
  styleUrls: ['./vendor-billing.component.scss']
})
export class VendorBillingComponent implements OnInit {

  @ViewChild(MatSort) sort: MatSort;

  // for the UI to access Enum
  public IntakeStatus = IntakeStatus;

  private intakes: IntakeForm[];
  private changes: IntakeForm[] = [];
  private unsubscribe$ = new Subject();
  public datasource: MatTableDataSource<TableRow>;
  public columnsToDisplay = ['intakeFormId', 'createdOn', 'status', 'patientName', 'patientState', 'vendorName', 'vendorBilled', 'vendorPaid', 'download'];

  constructor(
    private readonly patientApi: PatientService,
    private readonly intakeApi: IntakeFormService) { }

  ngOnInit() {
    this.intakeApi.getAll()
      .pipe(
        map((intakes: IntakeForm[]) => intakes.filter(v => v.status >= IntakeStatus.Approved)),
        takeUntil(this.unsubscribe$))
      .subscribe((intakes: IntakeForm[]) => {

        if (intakes.length > 0) {

          this.intakes = intakes;
          const patientIds = intakes.map(({ patientId }) => patientId);
          const patients$ = this.patientApi.getList(patientIds);

          patients$.subscribe((patients: Patient[]) => {

            const data: TableRow[] = [];

            intakes.forEach((intake: IntakeForm) => {
              const patient = patients.find(p => p.patientId === intake.patientId);
              const row = this.buildTableRow(intake, patient);
              data.push(row);
            });

            this.datasource = new MatTableDataSource(data);
            this.datasource.sort = this.sort;

          });

        }
      });
  }

  buildTableRow(intake: IntakeForm, patient: Patient): TableRow {
    const row = new TableRow();
    row.intakeFormId = intake.intakeFormId;
    row.status = intake.status;
    row.patientName = patient.firstName + ' ' + patient.lastName;
    row.patientState = patient.address.state;
    row.vendorPaid = intake.vendorPaid;
    row.vendorBilled = intake.vendorBilled;
    row.createdOn = intake.createdOn;

    return row;
  }


  /*

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

  }*/

}
