import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { AgentAccountFormComponent } from './account-form/agent-account-form.component';
import { CreateAgentComponent } from './create/create-agent.component';
import { AgentDashboardComponent } from './dashboard/agent-dashboard.component';
import { DmaDialogComponent } from './dma-dialog/dma-dialog.component';
import { EditAgentComponent } from './edit/edit-agent.component';
import { OrderComponent } from './order/order.component';

@NgModule({
  declarations: [
    CreateAgentComponent,
    EditAgentComponent,
    AgentDashboardComponent,
    OrderComponent,
    AgentAccountFormComponent,
    DmaDialogComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ],
  entryComponents: [
    DmaDialogComponent
  ]
})
export class AgentModule { }
