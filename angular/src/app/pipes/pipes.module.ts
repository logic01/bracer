import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { DocumentStatusPipe } from './document-status.pipe';
import { DocumentTypePipe } from './document-type.pipe';
import { PhonePipe } from './phone.pipe';

@NgModule({
  declarations: [
    PhonePipe,
    DocumentStatusPipe,
    DocumentTypePipe],
  imports: [CommonModule],
  exports: [
    PhonePipe,
    DocumentStatusPipe,
    DocumentTypePipe]
})
export class PipesModule { }
