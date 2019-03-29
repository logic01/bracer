import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { AgentService } from 'src/app/api/agent.service';
import { RouteUrls } from 'src/app/constants/routes';
import { Agent } from 'src/app/models/Agent.model';

@Component({
  selector: 'app-agent-list',
  templateUrl: './agent-list.component.html',
  styleUrls: ['./agent-list.component.scss']
})
export class AgentListComponent implements OnInit, OnDestroy {

  private unsubscribe$ = new Subject();

  public columnsToDisplay = ['userId', 'userName', 'firstName', 'lastName', 'edit'];

  public data: Agent[];

  constructor(
    private readonly agentApi: AgentService,
    private readonly router: Router) { }

  ngOnInit() {
    this.agentApi
      .getAll()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((physicianList: Agent[]) => {
        this.data = physicianList;
      });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  edit(id: number) {
    this.router.navigate(['/agent/edit', id]);
  }

  add() {
    this.router.navigateByUrl(RouteUrls.AgentCreateComponent);
  }
}
