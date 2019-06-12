import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-physician-dashboard',
  templateUrl: './physician-dashboard.component.html',
  styleUrls: ['./physician-dashboard.component.scss']
})
export class PhysicianDashboardComponent implements OnInit {

  columnsToDisplay = ['intakeFormId', 'status', 'view'];

  data: IntakeForm[];

  private unsubscribe$ = new Subject();

  constructor(
    private readonly session: SessionService,
    private readonly intakeFormApi: IntakeFormService) { }

  ngOnInit() {

    this.session.userAccount$.subscribe((account: UserAccount) => {

      this.intakeFormApi
        .getByPhysician(account.userAccountId)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe((intakeFormList: IntakeForm[]) => {
          this.data = intakeFormList;
        });

    });

  }

  /* good code use somewhere else
  download(id: string) {
    window.location.href = `${environment.api_url}/document/${id}/download`;
  }
  */

  view(id: string) {

  }



}
