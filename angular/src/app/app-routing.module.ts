import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreateAdminComponent } from './account/create/admin/create-admin.component';
import { CreatePhysicianComponent } from './account/create/physician/create-physician.component';
import { TypeSelectionComponent } from './account/create/type-selection/type-selection.component';
import { CreateVendorComponent } from './account/create/vendor/create-vendor.component';
import { InventoryComponent } from './inventory/inventory.component';
import { LoginComponent } from './login/login.component';
import { WelcomeComponent } from './welcome/welcome.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'welcome', component: WelcomeComponent },
  { path: 'inventory', component: InventoryComponent },
  { path: 'account/create/type-selection', component: TypeSelectionComponent },
  { path: 'account/create/admin', component: CreateAdminComponent },
  { path: 'account/create/vendor', component: CreateVendorComponent },
  { path: 'account/create/physician', component: CreatePhysicianComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
