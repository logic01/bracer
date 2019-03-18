import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {

  public searchForm: FormGroup;

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
  constructor(private readonly router: Router) { }

  ngOnInit() {
    this.searchForm = new FormGroup({
      username: new FormControl('', Validators.required),
      document_id: new FormControl('', Validators.required),
    });
  }

  clear() {
    this.data = [];
  }
  onSubmit() {
    // todo - add search
    this.data = [
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
  }

  view(id: number, type: string) {
    console.log(id);
    console.log(type);
  }

}
