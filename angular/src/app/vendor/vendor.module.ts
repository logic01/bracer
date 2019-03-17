import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DesignModule } from '../design/design.module';
import { ReactiveFormsModule } from '@angular/forms';
import { EditVendorComponent } from './edit/edit-vendor.component';
import { CreateVendorComponent } from './create/create-vendor.component';

@NgModule({
  declarations: [
    EditVendorComponent,
    CreateVendorComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ]
})
export class VendorModule { }
