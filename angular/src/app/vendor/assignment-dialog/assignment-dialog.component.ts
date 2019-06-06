import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Observable, Subject } from 'rxjs';
import { flatMap, map, takeUntil } from 'rxjs/operators';
import { Document } from 'src/app/models/document.model';
import { DocumentStatus } from 'src/app/models/enums/document-status.enum';
import { Physician } from 'src/app/models/physician.model';
import { DocumentService } from 'src/app/services/api/document.service';
import { PhysicianService } from 'src/app/services/api/physician.service';

export class AssignmentDialogData {
  documentId: string;
}

@Component({
  selector: 'app-assignment-dialog',
  templateUrl: './assignment-dialog.component.html',
  styleUrls: ['./assignment-dialog.component.scss']
})
export class AssignmentDialogComponent implements OnInit, OnDestroy {


  public form: FormGroup;
  public physicians$: Observable<Physician[]>;
  public document$: Observable<Document>;
  private unsubscribe$ = new Subject();

  constructor(
    private readonly physicianApi: PhysicianService,
    private readonly documentApi: DocumentService,
    public dialogRef: MatDialogRef<AssignmentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AssignmentDialogData) { }

  ngOnInit() {

    // get physicians and filter them so only active show.
    this.physicians$ = this.physicianApi.getAll().pipe(map(physician => physician.filter(p => p.userAccount.active)));

    this.document$ = this.documentApi.get(this.data.documentId);

    this.form = new FormGroup({
      physician: new FormControl('', Validators.required),
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  cancel(): void {
    this.dialogRef.close();
  }

  save() {

    const physicianId = this.form.controls['physician'].value;

    this.document$
      .pipe(
        takeUntil(this.unsubscribe$),
        flatMap((document: Document) => {

          document.physicianId = physicianId;
          document.status = DocumentStatus.Assigned;
          return this.documentApi.put(document);

        }))
      .subscribe((document: Document) => {
        this.dialogRef.close();
      });

  }
}
