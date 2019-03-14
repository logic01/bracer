import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminDashboardComponent } from './dashboard/admin-dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DesignModule } from '../design/design.module';

@NgModule({
  declarations: [AdminDashboardComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class AdminModule { }
