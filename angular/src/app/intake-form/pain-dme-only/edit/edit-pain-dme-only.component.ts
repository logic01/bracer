import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { forkJoin, Observable, Subject } from 'rxjs';

import { RouteUrls } from 'src/app/constants/routes';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';

@Component({
  selector: 'app-edit-pain-dme-only',
  templateUrl: './edit-pain-dme-only.component.html',
  styleUrls: ['./edit-pain-dme-only.component.scss']
})
export class EditPainDmeOnlyComponent implements OnInit, OnDestroy {

  private intakeId: string;
  public intake$: Observable<IntakeForm>;
  private unsubscribe$ = new Subject();

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly intakeApi: IntakeFormService
  ) { }

  ngOnInit() {
    this.intakeId = this.route.snapshot.paramMap.get('intakeFormId');
    this.intake$ = this.intakeApi.get(this.intakeId);
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit(intakes: IntakeForm[]) {

    const observables = [];

    intakes.forEach((intake: IntakeForm) => observables.push(this.intakeApi.put(intake.intakeFormId, intake)));

    // call the api for all the intake forms
    forkJoin(observables).subscribe(() => this.router.navigateByUrl(RouteUrls.AgentDashboardComponent));
  }

}
