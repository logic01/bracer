import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DesignModule } from '../design/design.module';
import { TypeSelectionComponent } from './type-selection/type-selection.component';

@NgModule({
  declarations: [
    TypeSelectionComponent
  ],
  imports: [
    CommonModule,
    DesignModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
