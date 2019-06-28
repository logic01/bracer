import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BillingDashboardComponent } from './admin/billing/billing-dashboard/billing-dashboard.component';
import { CreateAdminComponent } from './admin/create/create-admin.component';
import { AdminDashboardComponent } from './admin/dashboard/admin-dashboard.component';
import { EditAdminComponent } from './admin/edit/edit-admin.component';
import { CreateAgentComponent } from './agent/create/create-agent.component';
import { AgentDashboardComponent } from './agent/dashboard/agent-dashboard.component';
import { EditAgentComponent } from './agent/edit/edit-agent.component';
import { RouteUrls } from './constants/routes';
import { DocumentComponent } from './document/document.component';
import { IntakeFormComponent } from './intake-form/intake-form/intake-form.component';
import { LoginComponent } from './login/login.component';
import { AccountType } from './models/enums/account-type.enum';
import { CreatePatientComponent } from './patient/create/create-patient.component';
import { CreatePhysicianComponent } from './physician/create/create-physician.component';
import { PhysicianDashboardComponent } from './physician/dashboard/physician-dashboard.component';
import { EditPhysicianComponent } from './physician/edit/edit-physician.component';
import { RoleGuardService } from './services/role-guard.service';
import { SignInGuardService } from './services/sign-in-guard.service';
import { CreateVendorComponent } from './vendor/create/create-vendor.component';
import { EditVendorComponent } from './vendor/edit/edit-vendor.component';
import { ViewVendorComponent } from './vendor/view/view-vendor.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: RouteUrls.LoginComponent, component: LoginComponent },
  {
    path: RouteUrls.VendorIntakeDocumentComponent, component: DocumentComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.BillingDashboardComponent, component: BillingDashboardComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.PhysicianIntakeDocumentComponent, component: DocumentComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Physician] }
  },
  {
    path: RouteUrls.AdminCreateComponent, component: CreateAdminComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.PhysicianCreateComponent, component: CreatePhysicianComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.AgentCreateComponent, component: CreateAgentComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.IntakeFormComponent, component: IntakeFormComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Agent] }
  },
  {
    path: RouteUrls.PatientCreateComponent, component: CreatePatientComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Agent] }
  },
  {
    path: RouteUrls.AgentDashboardComponent, component: AgentDashboardComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Agent] }
  },
  {
    path: RouteUrls.PhysicianDashboardComponent, component: PhysicianDashboardComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Physician] }
  },
  {
    path: RouteUrls.AdminDashboardComponent, component: AdminDashboardComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.VendorCreateComponent, component: CreateVendorComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.VendorViewComponent, component: ViewVendorComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.AdminEditComponent, component: EditAdminComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.AgentEditComponent, component: EditAgentComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.PhysicianEditComponent, component: EditPhysicianComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: RouteUrls.VendorEditComponent, component: EditVendorComponent,
    canActivate: [RoleGuardService, SignInGuardService],
    data: { expectedRoles: [AccountType.Admin] }
  },
  {
    path: '**',
    redirectTo: RouteUrls.LoginComponent,
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
