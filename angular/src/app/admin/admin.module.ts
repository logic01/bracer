import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { AdminAccountFormComponent } from './account-form/admin-account-form.component';
import { CreateAdminComponent } from './create/create-admin.component';
import { AdminDashboardComponent } from './dashboard/admin-dashboard.component';
import { EditAdminComponent } from './edit/edit-admin.component';
import { AdminListComponent } from './tables/admin-list/admin-list.component';
import { AgentListComponent } from './tables/agent-list/agent-list.component';
import { DocumentListComponent } from './tables/document-list/document-list.component';
import { OrderListComponent } from './tables/order-list/order-list.component';
import { PhysicianListComponent } from './tables/physician-list/physician-list.component';
import { VendorListComponent } from './tables/vendor-list/vendor-list.component';


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
