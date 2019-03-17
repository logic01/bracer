import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';

import { AdminDashboardComponent } from './dashboard/admin-dashboard.component';
import { CreateAdminComponent } from './create/create-admin.component';
import { EditAdminComponent } from './edit/edit-admin.component';


@NgModule({
  declarations: [
    AdminDashboardComponent,
    CreateAdminComponent,
    EditAdminComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class AdminModule { }
