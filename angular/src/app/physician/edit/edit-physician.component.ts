import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { Subject, Observable } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from '../../constants/routes';
import { PhysicianService } from 'src/app/api/physician.service';
import { Physician } from 'src/app/models/physician.model';
import { UserAccount } from 'src/app/models/user-account.model';


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
