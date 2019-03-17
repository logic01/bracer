import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { AgentDashboardComponent } from './dashboard/agent-dashboard.component';
import { EditAgentComponent } from './edit/edit-agent.component';
import { CreateAgentComponent } from './create/create-agent.component';
import { CallComponent } from './call/call.component';

@NgModule({
  declarations: [
    CreateAgentComponent,
    EditAgentComponent,
    AgentDashboardComponent,
    CallComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class AgentModule { }
