import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { SignaturePadModule } from 'angular2-signaturepad';

import { DesignModule } from '../design/design.module';
import { PipesModule } from '../pipes/pipes.module';
import { DocumentComponent } from './document.component';
import { IntakeComponent } from './intake/intake.component';
import { PrescriptionComponent } from './prescription/prescription.component';
import { SignatureDialogComponent } from './signature-dialog/signature-dialog.component';

@NgModule({
  declarations: [
    DocumentComponent,
    IntakeComponent,
    PrescriptionComponent,
    SignatureDialogComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule,
    PipesModule,
    SignaturePadModule
  ],
  entryComponents: [
    SignatureDialogComponent
  ]
})
export class DocumentModule { }
