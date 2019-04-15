import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { AdminService } from '../../api/admin.service';
import { RouteUrls } from '../../constants/routes';
import { UserAccount } from '../../models/user-account.model';
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
