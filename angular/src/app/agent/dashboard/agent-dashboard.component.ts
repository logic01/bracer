import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { RouteUrls } from '../../constants/routes';

@Component({
  selector: 'app-agent-dashboard',
  templateUrl: './agent-dashboard.component.html',
  styleUrls: ['./agent-dashboard.component.scss']
})
export class AgentDashboardComponent implements OnInit {

  constructor(private readonly router: Router) { }

  ngOnInit() {
  }

  new_patient_button_click() {
    this.router.navigateByUrl(RouteUrls.PatientCreateComponent);
  }
}
