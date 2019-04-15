import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from '../../constants/routes';
import { AdminService } from '../../services/api/admin.service';
import { Admin } from 'src/app/models/admin.model';

@Component({
  selector: 'app-create-admin',
  templateUrl: './create-admin.component.html',
  styleUrls: ['./create-admin.component.scss']
})
export class CreateAdminComponent implements OnDestroy {

  private unsubscribe$ = new Subject();
  constructor(
    private readonly adminApi: AdminService,
    private readonly router: Router) { }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit(admin: Admin) {
    this.adminApi
      .post(admin)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newAdmin: Admin) => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });
  }

}
