import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { DesignModule } from '../design/design.module';
import { WelcomeComponent } from './welcome.component';

@NgModule({
  declarations: [WelcomeComponent],
  imports: [
    CommonModule,
    DesignModule
  ]
})
export class WelcomeModule { }
