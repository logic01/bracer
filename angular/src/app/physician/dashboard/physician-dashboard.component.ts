import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-physician-dashboard',
  templateUrl: './physician-dashboard.component.html',
  styleUrls: ['./physician-dashboard.component.scss']
})
export class PhysicianDashboardComponent implements OnInit {

  columnsToDisplay = ['documentId', 'userName', 'firstName', 'lastName', 'view'];

  data = [
    {
      documentId: '1001',
      userName: 'bpohl',
      firstName: 'brandon',
      lastName: 'pohl',
      view: 'true'
    },
    {
      documentId: '1002',
      userName: 'lpohl',
      firstName: 'leabeth',
      lastName: 'pohl',
      view: 'true'
    },
    {
      documentId: '1003',
      userName: 'lpohl',
      firstName: 'leabeth',
      lastName: 'pohl',
      view: 'true'
    }
  ];

  constructor() { }

  ngOnInit() {
    // renderRows() - to update table
  }

  view(id: number) {
    console.log(id);
  }
}
