import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
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

  @ViewChild(MatSort) sort: MatSort;

  private unsubscribe$ = new Subject();

  public datasource: MatTableDataSource<Admin>;
  public columnsToDisplay = ['userId', 'userName', 'firstName', 'lastName', 'edit'];

  constructor(
    private readonly adminApi: AdminService,
    private readonly router: Router) { }

  ngOnInit() {
    this.adminApi
      .getAll()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((admins: Admin[]) => {
        this.datasource = new MatTableDataSource(admins);
        this.datasource.sort = this.sort;
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
