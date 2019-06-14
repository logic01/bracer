import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { SessionService } from 'src/app/services/session.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-physician-dashboard',
  templateUrl: './physician-dashboard.component.html',
  styleUrls: ['./physician-dashboard.component.scss']
})
export class PhysicianDashboardComponent implements OnInit {

  @ViewChild(MatSort) sort: MatSort;

  columnsToDisplay = ['intakeFormId', 'status', 'view', 'download'];

  dataSource: MatTableDataSource<IntakeForm>;

  private unsubscribe$ = new Subject();

  constructor(
    private readonly router: Router,
    private readonly session: SessionService,
    private readonly intakeFormApi: IntakeFormService) { }

  ngOnInit() {


    this.session.userAccount$.subscribe((account: UserAccount) => {

      this.intakeFormApi
        .getByPhysician(account.userAccountId)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe((intakeFormList: IntakeForm[]) => {
          this.dataSource = new MatTableDataSource(intakeFormList);
          this.dataSource.sort = this.sort;
        });

    });

  }

  download(id: string) {
    window.location.href = `${environment.api_url}/document/${id}/download`;
  }

  view(id: string) {
    this.router.navigate(['intake-document/', id]);
  }

}
