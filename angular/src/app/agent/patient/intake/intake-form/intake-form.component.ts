import { Component, OnInit } from '@angular/core';

import { IntakeForm } from 'src/app/models/intake-form.model';

@Component({
  selector: 'app-intake-form',
  templateUrl: './intake-form.component.html',
  styleUrls: ['./intake-form.component.scss']
})
export class IntakeFormComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  onSubmit(intakeForm: IntakeForm) {

    console.warn(intakeForm);
  }

}
