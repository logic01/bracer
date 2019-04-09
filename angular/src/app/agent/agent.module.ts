import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { AgentAccountFormComponent } from './account-form/agent-account-form.component';
import { CreateAgentComponent } from './create/create-agent.component';
import { AgentDashboardComponent } from './dashboard/agent-dashboard.component';
import { EditAgentComponent } from './edit/edit-agent.component';
import { DmaDialogComponent } from './order/dma-dialog/dma-dialog.component';
import { AntiFungalRxOnlyComponent } from './order/intake/anti-fungal-rx-only/anti-fungal-rx-only.component';
import { FootbathRxOnlyComponent } from './order/intake/footbath-rx-only/footbath-rx-only.component';
import { GeneralDmeAndRxComponent } from './order/intake/general-dme-and-rx/general-dme-and-rx.component';
import { GeneralRxOnlyComponent } from './order/intake/general-rx-only/general-rx-only.component';
import { HeartburnAcidRxOnlyComponent } from './order/intake/heartburn-acid-rx-only/heartburn-acid-rx-only.component';
import { IntakeFormComponent } from './order/intake/intake-form/intake-form.component';
import { PainDmeOnlyComponent } from './order/intake/pain-dme-only/pain-dme-only.component';
import { PainRxOnlyComponent } from './order/intake/pain-rx-only/pain-rx-only.component';
import { RashSkinRxOnlyComponent } from './order/intake/rash-skin-rx-only/rash-skin-rx-only.component';
import { ScarRxOnlyComponent } from './order/intake/scar-rx-only/scar-rx-only.component';
import { MedicalInformationComponent } from './order/medical-information/medical-information.component';
import { OrderComponent } from './order/order/order.component';
import { PatientInformationComponent } from './order/patient-information/patient-information.component';
import { GeneralDmeOnlyComponent } from './order/intake/general-dme-only/general-dme-only.component';

@NgModule({
  declarations: [
    CreateAgentComponent,
    EditAgentComponent,
    AgentDashboardComponent,
    AgentAccountFormComponent,
    DmaDialogComponent,
    PatientInformationComponent,
    MedicalInformationComponent,
    OrderComponent,
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
    GeneralDmeOnlyComponent
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
