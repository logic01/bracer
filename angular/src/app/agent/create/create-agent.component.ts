import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { AgentService } from 'src/app/api/agent.service';
import { VendorService } from 'src/app/api/vendor.service';
import { RouteUrls } from 'src/app/constants/routes';
import { Agent } from 'src/app/models/agent.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { Vendor } from 'src/app/models/vendor.model';

@Component({
  selector: 'app-create-agent',
  templateUrl: './create-agent.component.html',
  styleUrls: ['./create-agent.component.scss']
})
export class CreateAgentComponent implements OnInit, OnDestroy {

  public accountForm: FormGroup;
  public vendors$: Observable<Vendor[]>;

  private unsubscribe$ = new Subject();

  constructor(
    private readonly agentApi: AgentService,
    private readonly vendorApi: VendorService,
    private readonly router: Router) { }

  ngOnInit() {
    this.accountForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      confirmationPassword: new FormControl('', Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      vendor: new FormControl('', Validators.required)
    });

    this.vendors$ = this.vendorApi.getAll();
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit() {

    if (!this.accountForm.valid) {
      return;
    }

    const agent = this.buildAgent();

    this.agentApi
      .post(agent)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newAgent: Agent) => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });
  }

  private buildAgent(): Agent {

    const agent = new Agent();
    agent.userAccount = new UserAccount();

    agent.userAccount.userName = this.accountForm.controls['userName'].value;
    agent.userAccount.password = this.accountForm.controls['password'].value;
    agent.userAccount.confirmationPassword = this.accountForm.controls['confirmationPassword'].value;
    agent.firstName = this.accountForm.controls['firstName'].value;
    agent.lastName = this.accountForm.controls['lastName'].value;
    agent.vendorId = this.accountForm.controls['vendor'].value;
    return agent;
  }

}
