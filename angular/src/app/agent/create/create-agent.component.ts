import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from 'src/app/constants/routes';
import { Agent } from 'src/app/models/agent.model';
import { Vendor } from 'src/app/models/vendor.model';
import { AgentService } from 'src/app/services/api/agent.service';
import { VendorService } from 'src/app/services/api/vendor.service';

@Component({
  selector: 'app-create-agent',
  templateUrl: './create-agent.component.html',
  styleUrls: ['./create-agent.component.scss']
})
export class CreateAgentComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();
  public vendors$: Observable<Vendor[]>;

  constructor(
    private readonly agentApi: AgentService,
    private readonly vendorApi: VendorService,
    private readonly router: Router) { }

  ngOnInit() {
    this.vendors$ = this.vendorApi.getAll();
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
