import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CreatePhysicianComponent } from './create/create-physician.component';
import { EditPhysicianComponent } from './edit/edit-physician.component';

const routes: Routes = [
  {
    path: 'create', component: CreatePhysicianComponent
  },
  {
    path: 'edit/:id', component: EditPhysicianComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PhysicianRoutingModule { }
