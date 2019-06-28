import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { PipesModule } from '../pipes/pipes.module';
import { AdminAccountFormComponent } from './account-form/admin-account-form.component';
import { BillingDashboardComponent } from './billing/billing-dashboard/billing-dashboard.component';
import { PhysicianBillingComponent } from './billing/physician-billing/physician-billing.component';
import { VendorBillingComponent } from './billing/vendor-billing/vendor-billing.component';
import { CreateAdminComponent } from './create/create-admin.component';
import { AdminDashboardComponent } from './dashboard/admin-dashboard.component';
import { EditAdminComponent } from './edit/edit-admin.component';
import { AdminListComponent } from './tables/admin-list/admin-list.component';
import { AgentListComponent } from './tables/agent-list/agent-list.component';
import { PatientListComponent } from './tables/patient-list/patient-list.component';
import { PhysicianListComponent } from './tables/physician-list/physician-list.component';
import { VendorListComponent } from './tables/vendor-list/vendor-list.component';

@NgModule({
  declarations: [
    AdminDashboardComponent,
    CreateAdminComponent,
    EditAdminComponent,
    AgentListComponent,
    AdminListComponent,
    VendorListComponent,
    PhysicianListComponent,
    AdminAccountFormComponent,
    PatientListComponent,
    BillingDashboardComponent,
    PhysicianBillingComponent,
    VendorBillingComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule,
    PipesModule
  ]
})
export class AdminModule { }
