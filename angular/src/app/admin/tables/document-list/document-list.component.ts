import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-document-list',
  templateUrl: './document-list.component.html',
  styleUrls: ['./document-list.component.scss']
})
export class DocumentListComponent implements OnInit {

  columnsToDisplay = ['userId', 'userName', 'firstName', 'lastName', 'edit'];

  data = [
    {
      userId: '1001',
      userName: 'bpohl',
      firstName: 'brandon',
      lastName: 'pohl',
      typ: 'admin',
      edit: 'true'
    },
    {
      userId: '1002',
      userName: 'lpohl',
      firstName: 'leabeth',
      lastName: 'pohl',
      type: 'phyisician',
      edit: 'true'
    },
    {
      userId: '1003',
      userName: 'lpohl',
      firstName: 'leabeth',
      lastName: 'pohl',
      type: 'agent',
      edit: 'true'
    }
  ];

  constructor() { }

  ngOnInit() {
  }

  clear() {
    this.data = [];
  }

  view(id: number, type: string) {
    console.log(id);
    console.log(type);
  }
}
