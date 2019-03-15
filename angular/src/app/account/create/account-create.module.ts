import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { CreateAdminComponent } from './admin/create-admin.component';
import { CreateAgentComponent } from './agent/create-agent.component';
import { CreatePhysicianComponent } from './physician/create-physician.component';
import { TypeSelectionComponent } from './type-selection/type-selection.component';
import { DesignModule } from 'src/app/design/design.module';

@NgModule({
  declarations: [
    TypeSelectionComponent,
    CreateAdminComponent,
    CreatePhysicianComponent,
    CreateAgentComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class AccountCreateModule { }
