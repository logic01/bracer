import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { RouteUrls } from 'src/app/constants/routes';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';

@Component({
  selector: 'app-intake-form',
  templateUrl: './intake-form.component.html',
  styleUrls: ['./intake-form.component.scss']
})
export class IntakeFormComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();
  public hidden = true;

  constructor(
    private readonly intakeApi: IntakeFormService,
    private readonly router: Router
  ) { }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }


  onSubmit(intakeForm: IntakeForm) {

    console.warn(intakeForm);

    this.intakeApi
      .post(intakeForm)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((results: IntakeForm) => {
        this.router.navigateByUrl(RouteUrls.AgentDashboardComponent);
      });
  }

}
