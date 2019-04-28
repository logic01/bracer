import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from 'src/app/constants/routes';
import { Admin } from 'src/app/models/admin.model';
import { AdminService } from 'src/app/services/api/admin.service';

@Component({
  selector: 'app-admin-list',
  templateUrl: './admin-list.component.html',
  styleUrls: ['./admin-list.component.scss']
})
export class AdminListComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();

  public columnsToDisplay = ['userId', 'userName', 'firstName', 'lastName', 'edit'];

  public data: Admin[];

  constructor(
    private readonly adminApi: AdminService,
    private readonly router: Router) { }

  ngOnInit() {
    this.adminApi
      .getAll()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((physicianList: Admin[]) => {
        this.data = physicianList;
      });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  edit(adminId: number) {
    this.router.navigate(['/admin/edit', adminId]);
  }

  add() {
    this.router.navigateByUrl(RouteUrls.AdminCreateComponent);
  }
}
