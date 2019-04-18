import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateAdminComponent } from './admin/create/create-admin.component';
import { AdminDashboardComponent } from './admin/dashboard/admin-dashboard.component';
import { EditAdminComponent } from './admin/edit/edit-admin.component';
import { CreateAgentComponent } from './agent/create/create-agent.component';
import { AgentDashboardComponent } from './agent/dashboard/agent-dashboard.component';
import { EditAgentComponent } from './agent/edit/edit-agent.component';
import { CreatePatientComponent } from './agent/patient/create/create-patient.component';
import { IntakeFormComponent } from './agent/patient/intake/intake-form/intake-form.component';
import { RouteUrls } from './constants/routes';
import { LoginComponent } from './login/login.component';
import { CreatePhysicianComponent } from './physician/create/create-physician.component';
import { PhysicianDashboardComponent } from './physician/dashboard/physician-dashboard.component';
import { EditPhysicianComponent } from './physician/edit/edit-physician.component';
import { CreateVendorComponent } from './vendor/create/create-vendor.component';
import { EditVendorComponent } from './vendor/edit/edit-vendor.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: RouteUrls.LoginComponent, component: LoginComponent },
  { path: RouteUrls.AdminCreateComponent, component: CreateAdminComponent },
  { path: RouteUrls.PhysicianCreateComponent, component: CreatePhysicianComponent },
  { path: RouteUrls.AgentCreateComponent, component: CreateAgentComponent },
  { path: RouteUrls.IntakeFormComponent, component: IntakeFormComponent },
  { path: RouteUrls.PatientCreateComponent, component: CreatePatientComponent },
  {
    path: RouteUrls.AgentDashboardComponent, component: AgentDashboardComponent,
    // canActivate: [RoleGuardService],
    //  data: { expectedRole: AccountType.Agent }
  },
  {
    path: RouteUrls.PhysicianDashboardComponent, component: PhysicianDashboardComponent,
    // canActivate: [RoleGuardService],
    // data: { expectedRole: AccountType.Physician }
  },
  {
    path: RouteUrls.AdminDashboardComponent, component: AdminDashboardComponent,
    //  canActivate: [RoleGuardService],
    // data: { expectedRole: AccountType.Admin }
  },
  { path: RouteUrls.VendorCreateComponent, component: CreateVendorComponent },
  { path: RouteUrls.AdminEditComponent, component: EditAdminComponent },
  { path: RouteUrls.AgentEditComponent, component: EditAgentComponent },
  { path: RouteUrls.PhysicianEditComponent, component: EditPhysicianComponent },
  { path: RouteUrls.VendorEditComponent, component: EditVendorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
