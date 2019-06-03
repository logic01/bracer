import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from 'src/app/constants/routes';
import { Agent } from 'src/app/models/agent.model';
import { Vendor } from 'src/app/models/vendor.model';
import { AgentService } from 'src/app/services/api/agent.service';
import { VendorService } from 'src/app/services/api/vendor.service';


@Component({
  selector: 'app-edit-agent',
  templateUrl: './edit-agent.component.html',
  styleUrls: ['./edit-agent.component.scss']
})
export class EditAgentComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();
  public vendors$: Observable<Vendor[]>;
  public agent$: Observable<Agent>;
  private id: string;

  constructor(
    private readonly agentApi: AgentService,
    private readonly vendorApi: VendorService,
    private readonly route: ActivatedRoute,
    private readonly router: Router) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');

    this.vendors$ = this.vendorApi.getAll();
    this.agent$ = this.agentApi.get(this.id);
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit(agent: Agent) {
    this.agentApi
      .put(this.id, agent)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((newAgent: Agent) => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });
  }
}
