import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminDashboardComponent } from './admin/dashboard/admin-dashboard.component';
import { RouteUrls } from './constants/routes';
import { LoginComponent } from './login/login.component';
import { PhysicianDashboardComponent } from './physician/dashboard/physician-dashboard.component';
import { TypeSelectionComponent } from './shared/type-selection/type-selection.component';
import { CreateAdminComponent } from './admin/create/create-admin.component';
import { CreatePhysicianComponent } from './physician/create/create-physician.component';
import { CreateAgentComponent } from './agent/create/create-agent.component';
import { CreateVendorComponent } from './vendor/create/create-vendor.component';
import { CallComponent } from './agent/call/call.component';
import { AgentDashboardComponent } from './agent/dashboard/agent-dashboard.component';


const routes: Routes = [
  { path: RouteUrls.LoginComponent, component: LoginComponent },
  { path: RouteUrls.TypeSelectionComponent, component: TypeSelectionComponent },
  { path: RouteUrls.CreateAdminComponent, component: CreateAdminComponent },
  { path: RouteUrls.CreatePhysicianComponent, component: CreatePhysicianComponent },
  { path: RouteUrls.CreateAgentComponent, component: CreateAgentComponent },
  { path: RouteUrls.AgentCallComponent, component: CallComponent },
  { path: RouteUrls.AgentDashboardComponent, component: AgentDashboardComponent },
  { path: RouteUrls.PhysicianDashboardComponent, component: PhysicianDashboardComponent },
  { path: RouteUrls.AdminDashboardComponent, component: AdminDashboardComponent },
  { path: RouteUrls.CreateVendorComponent, component: CreateVendorComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }