import { Component, OnDestroy, OnInit } from '@angular/core';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { AdminService } from 'src/app/api/admin.service';
import { Admin } from 'src/app/models/admin.model';

@Component({
  selector: 'app-admin-list',
  templateUrl: './admin-list.component.html',
  styleUrls: ['./admin-list.component.scss']
})
export class AdminListComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();

  public columnsToDisplay = ['userId', 'userName', 'firstName', 'lastName', 'edit'];

  public data: Admin[];

  constructor(private readonly adminApi: AdminService) { }

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

  view(id: number, type: string) {
    console.log(id);
    console.log(type);
  }
}
