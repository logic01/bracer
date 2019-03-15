import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { CreateVendorComponent } from './create-vendor/create-vendor.component';
import { AdminDashboardComponent } from './dashboard/admin-dashboard.component';

@NgModule({
  declarations: [
    AdminDashboardComponent,
    CreateVendorComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class AdminModule { }
