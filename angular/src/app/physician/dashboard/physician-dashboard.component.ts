import { Component, OnInit } from '@angular/core';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { DocumentService } from 'src/app/services/api/document.service';
import { Document } from '../../models/document.model';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-physician-dashboard',
  templateUrl: './physician-dashboard.component.html',
  styleUrls: ['./physician-dashboard.component.scss']
})
export class PhysicianDashboardComponent implements OnInit {

  private unsubscribe$ = new Subject();

  physicianId = '1';
  columnsToDisplay = ['documentId'];

  documents: Document[];

  constructor(
    private readonly documentApi: DocumentService,
    private readonly router: Router) { }

  ngOnInit() {


    this.documentApi
      .get(this.physicianId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((documentList: Document[]) => {
        this.documents = documentList;
      });
  }

  view(id: number) {
    console.log(id);
  }
}
