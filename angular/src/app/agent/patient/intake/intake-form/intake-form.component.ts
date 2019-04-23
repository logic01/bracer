import { Component, OnInit, OnDestroy } from '@angular/core';

import { IntakeForm } from 'src/app/models/intake-form.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-intake-form',
  templateUrl: './intake-form.component.html',
  styleUrls: ['./intake-form.component.scss']
})
export class IntakeFormComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();

  constructor(private readonly intakeApi: IntakeFormService) { }

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

      });
  }

}
