import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { CreateAdminComponent } from './admin/create-admin.component';
import { CreatePhysicianComponent } from './physician/create-physician.component';
import { TypeSelectionComponent } from './type-selection/type-selection.component';
import { CreateVendorComponent } from './vendor/create-vendor.component';
import { SignaturePadModule } from 'angular2-signaturepad';
import { DesignModule } from 'src/app/design/design.module';

@NgModule({
  declarations: [
    TypeSelectionComponent,
    CreateAdminComponent,
    CreateVendorComponent,
    CreatePhysicianComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SignaturePadModule,
    DesignModule
  ]
})
export class AccountCreateModule { }
