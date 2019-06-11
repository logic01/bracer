import { Component, Input, OnInit } from '@angular/core';

import { IntakeForm } from 'src/app/models/intake-form.model';
import { Patient } from 'src/app/models/patient.model';
import { Physician } from 'src/app/models/physician.model';

export interface ICD10 {
  text: string;
}

export interface HCPCS {
  product: string;
  text: string;
}

@Component({
  selector: 'app-prescription',
  templateUrl: './prescription.component.html',
  styleUrls: ['./prescription.component.scss']
})
export class PrescriptionComponent implements OnInit {

  @Input() patient: Patient;
  @Input() physician: Physician;
  @Input() intakeForm: IntakeForm;

  @Input() product: string;
  @Input() diagnosis: ICD10[] = [];
  @Input() lcodeText: HCPCS[] = [];
  @Input() signatureDate = Date.now();
  @Input() dateOfService = Date.now();

  constructor() { }

  ngOnInit() {
  }

  getLCodes() {
    let text = '';
    for (const ltext of this.lcodeText) {
      text += ltext.text;
    }

    return text;
  }

  getAnswer(key: string) {

    let text = '';
    const questions = this.intakeForm.questions.filter(q => q.key === key);

    if (questions.length > 0) {
      for (const answer of questions[0].answers) {
        text += answer.text;
      }
    }

    return text;

  }

}
