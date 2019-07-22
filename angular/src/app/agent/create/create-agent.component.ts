import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { RouteUrls } from 'src/app/constants/routes';
import { Agent } from 'src/app/models/agent.model';
import { AgentService } from 'src/app/services/api/agent.service';
import { VendorService } from 'src/app/services/api/vendor.service';

@Component({
  selector: 'app-create-agent',
  templateUrl: './create-agent.component.html',
  styleUrls: ['./create-agent.component.scss']
})
export class CreateAgentComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();
  public vendors$ = this.vendorApi.getAll();

  constructor(
    private readonly agentApi: AgentService,
    private readonly vendorApi: VendorService,
    private readonly router: Router) { }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onSubmit(agent: Agent) {
    this.agentApi
      .post(agent)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => {
        this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
      });
  }
}
