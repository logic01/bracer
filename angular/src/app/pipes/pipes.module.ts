import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { IntakeStatusPipe } from './intake-status.pipe';
import { PhonePipe } from './phone.pipe';

@NgModule({
  declarations: [
    PhonePipe,
    IntakeStatusPipe
  ],
  imports: [CommonModule],
  exports: [
    PhonePipe,
    IntakeStatusPipe
  ]
})
export class PipesModule { }
