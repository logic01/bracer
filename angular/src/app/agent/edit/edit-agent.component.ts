import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { Subject, Observable } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { AgentService } from 'src/app/api/agent.service';
import { RouteUrls } from 'src/app/constants/routes';
import { Agent } from 'src/app/models/agent.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { Vendor } from 'src/app/models/vendor.model';
import { VendorService } from 'src/app/api/vendor.service';


@Component({
  selector: 'app-edit-agent',
  templateUrl: './edit-agent.component.html',
  styleUrls: ['./edit-agent.component.scss']
})
export class EditAgentComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();
  public vendors$: Observable<Vendor[]>;
  public agent$: Observable<Agent>;

  constructor(
    private readonly agentApi: AgentService,
    private readonly vendorApi: VendorService,
    private readonly route: ActivatedRoute,
    private readonly router: Router) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');

    this.vendors$ = this.vendorApi.getAll();
    this.agent$ = this.agentApi.get(id);
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit(agent: Agent) {
    this.agentApi
      .post(agent)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newAgent: Agent) => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });
  }
}
