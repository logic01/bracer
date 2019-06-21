import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Patient } from 'src/app/models/patient.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { PatientService } from 'src/app/services/api/patient.service';
import { SessionService } from 'src/app/services/session.service';

import { RouteUrls } from '../../constants/routes';

@Component({
  selector: 'app-agent-dashboard',
  templateUrl: './agent-dashboard.component.html',
  styleUrls: ['./agent-dashboard.component.scss']
})
export class AgentDashboardComponent implements OnInit {

  @ViewChild(MatSort) sort: MatSort;

  private unsubscribe$ = new Subject();
  public dataSource: MatTableDataSource<Patient>;
  public columnsToDisplay = ['patientId', 'createdOn', 'firstName', 'lastName', 'actions'];

  constructor(
    private readonly router: Router,
    private readonly session: SessionService,
    private readonly patientApi: PatientService) { }

  ngOnInit() {

    this.session.userAccount$.subscribe((account: UserAccount) => {

      this.patientApi
        .getByAgent(account.userAccountId)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe((patientList: Patient[]) => {
          this.dataSource = new MatTableDataSource(patientList);
          this.dataSource.sort = this.sort;
        });

    });
  }

  add() {
    this.router.navigateByUrl(RouteUrls.PatientCreateComponent);
  }
}
