import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { PipesModule } from '../pipes/pipes.module';
import { VendorAccountFormComponent } from './account-form/vendor-account-form.component';
import { CreateVendorComponent } from './create/create-vendor.component';
import { EditVendorComponent } from './edit/edit-vendor.component';
import { ViewVendorComponent } from './view/view-vendor.component';

@NgModule({
  declarations: [
    EditVendorComponent,
    CreateVendorComponent,
    VendorAccountFormComponent,
    ViewVendorComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule,
    PipesModule
  ]
})
export class VendorModule { }
