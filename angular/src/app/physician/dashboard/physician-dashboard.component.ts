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

  columnsToDisplay = ['documentId', 'type', 'status', 'view', 'sign'];

  data: Document[];

  private unsubscribe$ = new Subject();

  constructor(
    private readonly session: SessionService,
    private readonly documentApi: DocumentService,
    private readonly router: Router) { }

  ngOnInit() {

    this.session.userAccount$.subscribe((account: UserAccount) => {

      this.documentApi
        .getByPhysician(account.userAccountId)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe((documentList: Document[]) => {
          this.data = documentList;
        });

    });

  }

  view(id: string) {

    this.documentApi.download(id).subscribe((res: any) => {
      console.log('start download:', res);
      const url = window.URL.createObjectURL(res);
      const a = document.createElement('a');
      document.body.appendChild(a);
      a.setAttribute('style', 'display: none');
      a.href = url;
      a.download = res.filename;
      a.click();
      window.URL.revokeObjectURL(url);
      a.remove(); // remove the element
    });

  }

  sign(id: string) {
    console.log(id);
  }
}
