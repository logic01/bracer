import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { CreatePatientComponent } from './create/create-patient.component';
import { DmaDialogComponent } from './dma-dialog/dma-dialog.component';
import { EditPatientComponent } from './edit/edit.component';
import { PatientFormComponent } from './patient-form/patient-form.component';


@NgModule({
  declarations: [
    DmaDialogComponent,
    CreatePatientComponent,
    EditPatientComponent,
    PatientFormComponent
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
