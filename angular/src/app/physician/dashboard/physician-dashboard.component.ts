import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Document } from '../../models/document.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { DocumentService } from 'src/app/services/api/document.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-physician-dashboard',
  templateUrl: './physician-dashboard.component.html',
  styleUrls: ['./physician-dashboard.component.scss']
})
export class PhysicianDashboardComponent implements OnInit {

  private unsubscribe$ = new Subject();

  physicianId = '1';
  columnsToDisplay = ['documentId', 'type', 'status'];

  documents: Document[];

  constructor(
    private readonly session: SessionService,
    private readonly documentApi: DocumentService,
    private readonly router: Router) { }

  ngOnInit() {

    this.session.userAccount$.subscribe((account: UserAccount) => {

      this.documentApi
        .getAll(account.userId)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe((documentList: Document[]) => {
          this.documents = documentList;
        });

    });

  }

  view(id: number) {
    console.log(id);
  }
}
