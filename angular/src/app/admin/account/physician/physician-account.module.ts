import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DesignModule } from 'src/app/design/design.module';
import { PipesModule } from 'src/app/pipes/pipes.module';

import { CreatePhysicianComponent } from './create/create-physician.component';
import { EditPhysicianComponent } from './edit/edit-physician.component';
import { PhysicianAccountFormComponent } from './form/physician-account-form.component';
import { PhysicianRoutingModule } from './physician-account.routing.module';

@NgModule({
  declarations: [
    CreatePhysicianComponent,
    EditPhysicianComponent,
    PhysicianAccountFormComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule,
    PipesModule,
    PhysicianRoutingModule
  ]
})
export class PhysicianAccountModule { }
