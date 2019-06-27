import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { RouteUrls } from 'src/app/constants/routes';
import { Agent } from 'src/app/models/Agent.model';
import { AgentService } from 'src/app/services/api/agent.service';

@Component({
  selector: 'app-agent-list',
  templateUrl: './agent-list.component.html',
  styleUrls: ['./agent-list.component.scss']
})
export class AgentListComponent implements OnInit, OnDestroy {

  @ViewChild(MatSort) sort: MatSort;

  private unsubscribe$ = new Subject();

  public datasource: MatTableDataSource<Agent>;
  public columnsToDisplay = ['userId', 'userName', 'firstName', 'lastName', 'active', 'edit'];

  constructor(
    private readonly agentApi: AgentService,
    private readonly router: Router) { }

  ngOnInit() {
    this.agentApi
      .getAll()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((agent: Agent[]) => {
        this.datasource = new MatTableDataSource(agent);
        this.datasource.sort = this.sort;
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
