import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from '../../constants/routes';
import { PhysicianService } from 'src/app/api/physician.service';
import { Physician } from 'src/app/models/physician.model';


@Component({
  selector: 'app-create-physician',
  templateUrl: './create-physician.component.html',
  styleUrls: ['./create-physician.component.scss']
})
export class CreatePhysicianComponent implements OnDestroy {

  private unsubscribe$ = new Subject();

  constructor(
    private readonly physicianApi: PhysicianService,
    private readonly router: Router) { }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit(physician: Physician) {
    this.physicianApi
      .post(physician)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newPhysician: Physician) => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });
  }

}
