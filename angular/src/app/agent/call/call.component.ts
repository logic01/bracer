import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RouteUrls } from '../../constants/routes';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  styleUrls: ['./call.component.scss']
})
export class CallComponent implements OnInit {

  public callForm: FormGroup;

  constructor(private readonly router: Router) { }

  ngOnInit() {
    this.callForm = new FormGroup({
      first_name: new FormControl('', Validators.required),
      last_name: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    this.router.navigateByUrl(RouteUrls.AgentDashboardComponent);
  }
}
