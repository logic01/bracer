import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CallComponent } from './call/call.component';
import { VendorDashboardComponent } from './dashboard/vendor-dashboard.component';
import { DesignModule } from '../design/design.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [CallComponent, VendorDashboardComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class VendorModule { }
