import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateAdminComponent } from './account/create/admin/create-admin.component';
import { CreatePhysicianComponent } from './account/create/physician/create-physician.component';
import { TypeSelectionComponent } from './account/create/type-selection/type-selection.component';
import { CreateVendorComponent } from './account/create/vendor/create-vendor.component';
import { LoginComponent } from './login/login.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { VendorDashboardComponent } from './vendor/dashboard/vendor-dashboard.component';
import { CallComponent } from './vendor/call/call.component';
import { RouteUrls } from './constants/routes';
import { AdminDashboardComponent } from './admin/dashboard/admin-dashboard.component';
import { PhysicianDashboardComponent } from './physician/dashboard/physician-dashboard.component';


const routes: Routes = [
  { path: '', component: WelcomeComponent },
  { path: RouteUrls.WelcomeComponent, component: WelcomeComponent },
  { path: RouteUrls.LoginComponent, component: LoginComponent },
  { path: RouteUrls.TypeSelectionComponent, component: TypeSelectionComponent },
  { path: RouteUrls.CreateAdminComponent, component: CreateAdminComponent },
  { path: RouteUrls.CreateVendorComponent, component: CreateVendorComponent },
  { path: RouteUrls.CreatePhysicianComponent, component: CreatePhysicianComponent },
  { path: RouteUrls.VendorDashboardComponent, component: VendorDashboardComponent },
  { path: RouteUrls.VendorCallComponent, component: CallComponent },
  { path: RouteUrls.PhysicianDashboardComponent, component: PhysicianDashboardComponent },
  { path: RouteUrls.AdminDashboardComponent, component: AdminDashboardComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
