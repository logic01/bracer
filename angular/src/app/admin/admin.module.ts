import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';

import { AdminDashboardComponent } from './dashboard/admin-dashboard.component';
import { CreateAdminComponent } from './create/create-admin.component';
import { EditAdminComponent } from './edit/edit-admin.component';
import { PhysicianListComponent } from './physician-list/physician-list.component';
import { AgentListComponent } from './agent-list/agent-list.component';
import { AdminListComponent } from './admin-list/admin-list.component';
import { OrderListComponent } from './order-list/order-list.component';
import { DocumentListComponent } from './document-list/document-list.component';
import { VendorListComponent } from './vendor-list/vendor-list.component';


@NgModule({
  declarations: [
    AdminDashboardComponent,
    CreateAdminComponent,
    EditAdminComponent,
    PhysicianListComponent,
    AgentListComponent,
    AdminListComponent,
    OrderListComponent,
    DocumentListComponent,
    VendorListComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class AdminModule { }
