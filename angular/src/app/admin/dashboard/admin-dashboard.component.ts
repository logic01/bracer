import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {

  public searchForm: FormGroup;

  constructor(private readonly router: Router) { }

  ngOnInit() {
    this.searchForm = new FormGroup({
      username: new FormControl('', Validators.required),
      document_id: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    // todo - add search
  }

}
