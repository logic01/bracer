import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { InsuranceTypePipe } from './insurance-type.pipe';
import { IntakeStatusPipe } from './intake-status.pipe';
import { PhonePipe } from './phone.pipe';
import { SexPipe } from './sex-pipe';

@NgModule({
  declarations: [
    PhonePipe,
    IntakeStatusPipe,
    InsuranceTypePipe,
    SexPipe
  ],
  imports: [CommonModule],
  exports: [
    PhonePipe,
    IntakeStatusPipe,
    InsuranceTypePipe,
    SexPipe
  ]
})
export class PipesModule { }
