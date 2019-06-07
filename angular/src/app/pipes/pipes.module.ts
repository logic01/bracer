import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { IntakeStatusPipe } from './intake-status.pipe';
import { PhonePipe } from './phone.pipe';
import { SexPipe } from './sex-pipe';

@NgModule({
  declarations: [
    PhonePipe,
    IntakeStatusPipe,
    SexPipe
  ],
  imports: [CommonModule],
  exports: [
    PhonePipe,
    IntakeStatusPipe,
    SexPipe
  ]
})
export class PipesModule { }
