import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { PhysicianDashboardComponent } from './dashboard/physician-dashboard.component';
import { DocumentComponent } from './document/document.component';
import { SignaturePadModule } from 'angular2-signaturepad';
import { EditPhysicianComponent } from './edit/edit-physician.component';
import { CreatePhysicianComponent } from './create/create-physician.component';

@NgModule({
  declarations: [
    EditPhysicianComponent,
    CreatePhysicianComponent,
    PhysicianDashboardComponent,
    DocumentComponent
  ],
  imports: [
    CommonModule,
    DesignModule,
    ReactiveFormsModule,
    SignaturePadModule
  ]
})
export class PhysicianModule { }