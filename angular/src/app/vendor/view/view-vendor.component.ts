import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatTable } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { forkJoin, Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { DocumentStatus } from 'src/app/models/enums/document-status.enum';
import { DocumentType } from 'src/app/models/enums/document-type.enum';
import { Physician } from 'src/app/models/physician.model';
import { Vendor } from 'src/app/models/vendor.model';
import { DocumentService } from 'src/app/services/api/document.service';
import { PhysicianService } from 'src/app/services/api/physician.service';
import { VendorService } from 'src/app/services/api/vendor.service';

import { Document } from '../../models/document.model';
import { AssignmentDialogComponent } from '../assignment-dialog/assignment-dialog.component';

export class TableRow {
  documentId: string;
  intakeFormId: string;
  physicianName: string;
  physicianState: string;
  type: DocumentType;
  status: DocumentStatus;
}

@Component({
  selector: 'app-view-vendor',
  templateUrl: './view-vendor.component.html',
  styleUrls: ['./view-vendor.component.scss']
})
export class ViewVendorComponent implements OnInit, OnDestroy {

  @ViewChild(MatTable) table: MatTable<TableRow>;

  public vendor$: Observable<Vendor>;
  public physicians$: Observable<Physician[]>;
  public documents$: Observable<Document[]>;

  public data: TableRow[] = [];
  public columnsToDisplay = ['documentId', 'type', 'status', 'physicianName', 'physicianState', 'sign'];

  private unsubscribe$ = new Subject();

  constructor(
    private readonly physicianApi: PhysicianService,
    private readonly vendorApi: VendorService,
    private readonly documentApi: DocumentService,
    private readonly route: ActivatedRoute,
    private readonly dialog: MatDialog) {
  }

  ngOnInit() {

    const vendorId = this.route.snapshot.paramMap.get('id');

    this.vendor$ = this.vendorApi.get(vendorId);
    this.physicians$ = this.physicianApi.getAll();
    this.documents$ = this.documentApi.getByVendor(vendorId);

    forkJoin([this.documents$, this.physicians$])
      .pipe(
        takeUntil(this.unsubscribe$),
        map(responses => {

          const documents = responses[0];
          const physicians = responses[1];

          documents.forEach((doc: Document) => {

            const row = new TableRow();
            row.documentId = doc.documentId;
            row.type = doc.type;
            row.status = doc.status;
            row.physicianName = this.getPhysicianName(doc.physicianId, physicians);
            row.physicianState = this.getPhysicianState(doc.physicianId, physicians);
            this.data.push(row);
          });

          this.table.renderRows();
        }))
      .subscribe();

  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }


  getPhysicianName(physicianId: string, physicians: Physician[]): string {

    const physician = physicians.find(x => x.userAccount.userAccountId === physicianId);

    if (physician) {
      return `${physician.firstName} ${physician.lastName}`;
    }

    return '';
  }

  getPhysicianState(physicianId: string, physicians: Physician[]): string {

    const physician = physicians.find(x => x.userAccount.userAccountId === physicianId);

    if (physician) {
      return physician.address.state;
    }

    return '';
  }

  assign(documentId: string): void {
    const dialogRef = this.dialog.open(AssignmentDialogComponent, { data: { documentId: documentId } });
  }

  email(documentId: string): void {
    const dialogRef = this.dialog.open(AssignmentDialogComponent, { data: { documentId: documentId } });
  }

}
