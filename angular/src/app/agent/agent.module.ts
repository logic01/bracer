import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { AgentAccountFormComponent } from './account-form/agent-account-form.component';
import { CreateAgentComponent } from './create/create-agent.component';
import { AgentDashboardComponent } from './dashboard/agent-dashboard.component';
import { EditAgentComponent } from './edit/edit-agent.component';
import { CreatePatientComponent } from './patient/create/create-patient.component';
import { DmaDialogComponent } from './patient/dma-dialog/dma-dialog.component';
import { AntiFungalRxOnlyComponent } from './patient/intake/anti-fungal-rx-only/anti-fungal-rx-only.component';
import { FootbathRxOnlyComponent } from './patient/intake/footbath-rx-only/footbath-rx-only.component';
import { GeneralDmeAndRxComponent } from './patient/intake/general-dme-and-rx/general-dme-and-rx.component';
import { GeneralDmeOnlyComponent } from './patient/intake/general-dme-only/general-dme-only.component';
import { GeneralRxOnlyComponent } from './patient/intake/general-rx-only/general-rx-only.component';
import { HeartburnAcidRxOnlyComponent } from './patient/intake/heartburn-acid-rx-only/heartburn-acid-rx-only.component';
import { IntakeFormComponent } from './patient/intake/intake-form/intake-form.component';
import { PainDmeOnlyComponent } from './patient/intake/pain-dme-only/pain-dme-only.component';
import { PainRxOnlyComponent } from './patient/intake/pain-rx-only/pain-rx-only.component';
import { RashSkinRxOnlyComponent } from './patient/intake/rash-skin-rx-only/rash-skin-rx-only.component';
import { ScarRxOnlyComponent } from './patient/intake/scar-rx-only/scar-rx-only.component';

@NgModule({
  declarations: [
    CreateAgentComponent,
    EditAgentComponent,
    AgentDashboardComponent,
    AgentAccountFormComponent,
    DmaDialogComponent,
    GeneralRxOnlyComponent,
    GeneralDmeAndRxComponent,
    PainRxOnlyComponent,
    ScarRxOnlyComponent,
    HeartburnAcidRxOnlyComponent,
    RashSkinRxOnlyComponent,
    AntiFungalRxOnlyComponent,
    FootbathRxOnlyComponent,
    IntakeFormComponent,
    PainDmeOnlyComponent,
    GeneralDmeOnlyComponent,
    CreatePatientComponent
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
