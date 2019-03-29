import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';

import { AdminDashboardComponent } from './dashboard/admin-dashboard.component';
import { CreateAdminComponent } from './create/create-admin.component';
import { EditAdminComponent } from './edit/edit-admin.component';
import { AgentListComponent } from './tables/agent-list/agent-list.component';
import { AdminListComponent } from './tables/admin-list/admin-list.component';
import { OrderListComponent } from './tables/order-list/order-list.component';
import { DocumentListComponent } from './tables/document-list/document-list.component';
import { VendorListComponent } from './tables/vendor-list/vendor-list.component';
import { PhysicianListComponent } from './tables/physician-list/physician-list.component';
import { AdminAccountFormComponent } from './account-form/admin-account-form.component';


@NgModule({
  declarations: [
    AdminDashboardComponent,
    CreateAdminComponent,
    EditAdminComponent,
    AgentListComponent,
    AdminListComponent,
    OrderListComponent,
    DocumentListComponent,
    VendorListComponent,
    PhysicianListComponent,
    AdminAccountFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class AdminModule { }
