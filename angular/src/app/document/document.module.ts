import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { DesignModule } from '../design/design.module';
import { PipesModule } from '../pipes/pipes.module';
import { DocumentComponent } from './document.component';

@NgModule({
  declarations: [
    DocumentComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DesignModule,
    PipesModule
  ]
})
export class DocumentModule { }
