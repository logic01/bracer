import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from '../../constants/routes';
import { Physician } from 'src/app/models/physician.model';
import { PhysicianService } from 'src/app/services/api/physician.service';


@Component({
  selector: 'app-edit-physician',
  templateUrl: './edit-physician.component.html',
  styleUrls: ['./edit-physician.component.scss']
})
export class EditPhysicianComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();
  public physician$: Observable<Physician>;

  constructor(
    private readonly physicianApi: PhysicianService,
    private readonly route: ActivatedRoute,
    private readonly router: Router) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.physician$ = this.physicianApi.get(id);
  }

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
