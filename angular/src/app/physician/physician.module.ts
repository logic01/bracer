import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhysicianDashboardComponent } from './dashboard/physician-dashboard.component';
import { DesignModule } from '../design/design.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [PhysicianDashboardComponent],
  imports: [
    CommonModule,
    DesignModule,
    ReactiveFormsModule
  ]
})
export class PhysicianModule { }
