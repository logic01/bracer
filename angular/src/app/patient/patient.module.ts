import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { CreatePatientComponent } from './create/create-patient.component';
import { DmaDialogComponent } from './dma-dialog/dma-dialog.component';


@NgModule({
  declarations: [
    DmaDialogComponent,
    CreatePatientComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule
  ],
  entryComponents: [
    DmaDialogComponent
  ]
})
export class PatientModule { }
