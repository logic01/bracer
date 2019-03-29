import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DesignModule } from '../design/design.module';
import { ReactiveFormsModule } from '@angular/forms';
import { EditVendorComponent } from './edit/edit-vendor.component';
import { CreateVendorComponent } from './create/create-vendor.component';
import { VendorAccountFormComponent } from './account-form/vendor-account-form.component';

@NgModule({
  declarations: [
    EditVendorComponent,
    CreateVendorComponent,
    VendorAccountFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class VendorModule { }
