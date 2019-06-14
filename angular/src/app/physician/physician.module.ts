import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { PipesModule } from '../pipes/pipes.module';
import { PhysicianAccountFormComponent } from './account-form/physician-account-form.component';
import { CreatePhysicianComponent } from './create/create-physician.component';
import { PhysicianDashboardComponent } from './dashboard/physician-dashboard.component';
import { EditPhysicianComponent } from './edit/edit-physician.component';

@NgModule({
  declarations: [
    EditPhysicianComponent,
    CreatePhysicianComponent,
    PhysicianDashboardComponent,
    PhysicianAccountFormComponent
  ],
  imports: [
    CommonModule,
    DesignModule,
    ReactiveFormsModule,
    PipesModule
  ]
})
export class PhysicianModule { }
