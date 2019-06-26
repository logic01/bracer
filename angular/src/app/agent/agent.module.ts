import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { AgentAccountFormComponent } from './account-form/agent-account-form.component';
import { CreateAgentComponent } from './create/create-agent.component';
import { AgentDashboardComponent } from './dashboard/agent-dashboard.component';
import { EditAgentComponent } from './edit/edit-agent.component';



@NgModule({
  declarations: [
    CreateAgentComponent,
    EditAgentComponent,
    AgentDashboardComponent,
    AgentAccountFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class AgentModule { }
