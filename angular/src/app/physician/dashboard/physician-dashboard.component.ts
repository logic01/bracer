import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Signature } from 'src/app/models/signature.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { DocumentService } from 'src/app/services/api/document.service';
import { SignatureService } from 'src/app/services/api/signature.service';
import { IPAddressService } from 'src/app/services/ip-address.service';
import { SessionService } from 'src/app/services/session.service';
import { environment } from 'src/environments/environment';

import { Document } from '../../models/document.model';
import { SignatureDialogComponent } from '../signature-dialog/signature-dialog.component';

@Component({
  selector: 'app-physician-dashboard',
  templateUrl: './physician-dashboard.component.html',
  styleUrls: ['./physician-dashboard.component.scss']
})
export class PhysicianDashboardComponent implements OnInit {

  columnsToDisplay = ['documentId', 'type', 'status', 'download', 'sign'];

  data: Document[];

  private unsubscribe$ = new Subject();

  constructor(
    private readonly session: SessionService,
    private readonly documentApi: DocumentService,
    private readonly signatureApi: SignatureService,
    private readonly dialog: MatDialog,
    private readonly ipAddressService: IPAddressService) { }

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

  download(id: string) {
    window.location.href = `${environment.api_url}/document/${id}/download`;
  }

  sign(id: string) {
    const dialogRef = this.dialog.open(SignatureDialogComponent);

    dialogRef.afterClosed().subscribe(result => {

      const signature = new Signature();
      signature.content = result;
      signature.ipAddress = '71.205.117.185';
      this.signatureApi.sign(id, signature).subscribe();

      // to do -> find better pattern
      //  this.ipAddressService.getIpAddress().subscribe((ip: string) => {

      // });

    });
  }

}
