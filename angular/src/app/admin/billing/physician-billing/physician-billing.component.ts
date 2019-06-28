import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-physician-billing',
  templateUrl: './physician-billing.component.html',
  styleUrls: ['./physician-billing.component.scss']
})
export class PhysicianBillingComponent implements OnInit {


  @ViewChild(MatSort) sort: MatSort;

  private unsubscribe$ = new Subject();
  public dataSource: MatTableDataSource<IntakeForm>;
  public columnsToDisplay = ['intakeFormId', 'createdOn', 'status'];

  constructor(
    private readonly router: Router,
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
}
