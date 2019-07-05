import { Component, OnInit, ViewChild } from '@angular/core';
import { MatCheckboxChange, MatSort, MatTableDataSource } from '@angular/material';

import { forkJoin, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';

import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Physician } from 'src/app/models/physician.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { PhysicianService } from 'src/app/services/api/physician.service';
import { environment } from 'src/environments/environment';


export class TableRow {
  intakeFormId: string;
  createdOn: string;
  status: IntakeStatus;
  physicianName: string;
  physicianState: string;
  physicianPaid: boolean;
}

@Component({
  selector: 'app-physician-billing',
  templateUrl: './physician-billing.component.html',
  styleUrls: ['./physician-billing.component.scss']
})
export class PhysicianBillingComponent implements OnInit {

  @ViewChild(MatSort) sort: MatSort;

  // for the UI to access Enum
  public IntakeStatus = IntakeStatus;

  private intakes: IntakeForm[];
  private changes: IntakeForm[] = [];
  private unsubscribe$ = new Subject();
  public datasource: MatTableDataSource<TableRow>;
  public columnsToDisplay = ['intakeFormId', 'createdOn', 'status', 'physicianName', 'physicianState', 'physicianPaid', 'download'];


  constructor(
    private readonly physicianApi: PhysicianService,
    private readonly intakeApi: IntakeFormService) { }

  ngOnInit() {

    this.intakeApi.getAll()
      .pipe(
        map((intakes: IntakeForm[]) => intakes.filter(v => v.status >= IntakeStatus.Approved)),
        takeUntil(this.unsubscribe$))
      .subscribe((intakes: IntakeForm[]) => {

        if (intakes.length > 0) {

          this.intakes = intakes;
          const physicianIds = intakes.map(({ physicianId }) => physicianId);
          const physicians$ = this.physicianApi.getList(physicianIds);

          physicians$.subscribe((physicians: Physician[]) => {

            const data: TableRow[] = [];

            intakes.forEach((intake: IntakeForm) => {
              const physician = physicians.find(p => p.userAccount.userAccountId === intake.physicianId);
              const row = this.buildTableRow(intake, physician);
              data.push(row);
            });

            this.datasource = new MatTableDataSource(data);
            this.datasource.sort = this.sort;

          });

        }
      });
  }

  buildTableRow(intake: IntakeForm, physician: Physician): TableRow {
    const row = new TableRow();
    row.intakeFormId = intake.intakeFormId;
    row.status = intake.status;
    row.physicianName = physician.firstName + ' ' + physician.lastName;
    row.physicianState = physician.address.state;
    row.physicianPaid = intake.physicianPaid;
    row.createdOn = intake.createdOn;

    return row;
  }

  paid(event: MatCheckboxChange, intakeFormId: string) {

    const change = this.changes.find((intake: IntakeForm) => intake.intakeFormId === intakeFormId);

    if (change) {
      change.physicianPaid = event.checked;
      const index = this.changes.indexOf(change);
      this.changes[index] = change;
    } else {
      const newChange = this.intakes.find((intake: IntakeForm) => intake.intakeFormId === intakeFormId);
      newChange.physicianPaid = event.checked;
      this.changes.push(newChange);
    }

  }

  save() {

    const observables = [];

    this.changes.forEach((intake: IntakeForm) => observables.push(this.intakeApi.put(intake.intakeFormId, intake)));

    forkJoin(observables).subscribe();
  }


  download(documentId: string) {
    window.location.href = `${environment.api_url}/document/${documentId}/download`;
  }

}
