import { Component, OnDestroy, OnInit } from '@angular/core';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { AgentService } from 'src/app/api/agent.service';
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

  constructor(private readonly agentApi: AgentService) { }

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

  view(id: number, type: string) {
    console.log(id);
    console.log(type);
  }
}
