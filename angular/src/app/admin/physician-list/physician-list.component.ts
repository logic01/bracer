import { Component, OnDestroy, OnInit } from '@angular/core';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { PhysicianService } from 'src/app/api/physician.service';
import { Physician } from 'src/app/models/physician.model';

@Component({
  selector: 'app-physician-list',
  templateUrl: './physician-list.component.html',
  styleUrls: ['./physician-list.component.scss']
})
export class PhysicianListComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();

  public columnsToDisplay = ['userId', 'userName', 'firstName', 'lastName', 'phoneNumber', 'edit'];

  public data: Physician[];

  constructor(private readonly physicianApi: PhysicianService) { }

  ngOnInit() {
    this.physicianApi
      .getAll()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((physicianList: Physician[]) => {
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