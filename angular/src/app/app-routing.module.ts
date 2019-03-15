import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateAdminComponent } from './account/create/admin/create-admin.component';
import { CreateAgentComponent } from './account/create/agent/create-agent.component';
import { CreatePhysicianComponent } from './account/create/physician/create-physician.component';
import { TypeSelectionComponent } from './account/create/type-selection/type-selection.component';
import { CreateVendorComponent } from './admin/create-vendor/create-vendor.component';
import { AdminDashboardComponent } from './admin/dashboard/admin-dashboard.component';
import { RouteUrls } from './constants/routes';
import { LoginComponent } from './login/login.component';
import { PhysicianDashboardComponent } from './physician/dashboard/physician-dashboard.component';
import { CallComponent } from './vendor/call/call.component';
import { VendorDashboardComponent } from './vendor/dashboard/vendor-dashboard.component';


const routes: Routes = [
  { path: RouteUrls.LoginComponent, component: LoginComponent },
  { path: RouteUrls.TypeSelectionComponent, component: TypeSelectionComponent },
  { path: RouteUrls.CreateAdminComponent, component: CreateAdminComponent },
  { path: RouteUrls.CreatePhysicianComponent, component: CreatePhysicianComponent },
  { path: RouteUrls.CreateAgentComponent, component: CreateAgentComponent },
  { path: RouteUrls.VendorDashboardComponent, component: VendorDashboardComponent },
  { path: RouteUrls.VendorCallComponent, component: CallComponent },
  { path: RouteUrls.PhysicianDashboardComponent, component: PhysicianDashboardComponent },
  { path: RouteUrls.AdminDashboardComponent, component: AdminDashboardComponent },
  { path: RouteUrls.CreateVendorComponent, component: CreateVendorComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
