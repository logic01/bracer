import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { Answer } from 'src/app/models/answer.model';
import { IntakeFormType } from 'src/app/models/enums/intake-form-type.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Question } from 'src/app/models/question.model';

@Component({
  selector: 'app-anti-fungal-rx-only',
  templateUrl: './anti-fungal-rx-only.component.html',
  styleUrls: ['./anti-fungal-rx-only.component.scss']
})
export class AntiFungalRxOnlyComponent implements OnInit {

  @Output() formSubmitEvent = new EventEmitter<IntakeForm>();

  form: FormGroup;

  constructor() { }

  ngOnInit() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q2: new FormControl('', Validators.required),
      q3: new FormControl('', Validators.required)
    });
  }

  onSubmit() {

    if (!this.form.valid) {
      return;
    }

    const intakeForm = this.buildIntake();

    this.formSubmitEvent.emit(intakeForm);
  }

  buildIntake(): IntakeForm {

    const question1 = new Question();
    const answer1 = new Answer();
    answer1.text = this.form.controls['q1'].value;
    question1.answers = [answer1];

    const question2 = new Question();
    const answer2 = new Answer();
    answer2.text = this.form.controls['q2'].value;
    question2.answers = [answer2];

    const question3 = new Question();
    const answer3 = new Answer();
    answer3.text = this.form.controls['q3'].value;
    question3.answers = [answer3];

    const intake = new IntakeForm();
    intake.intakeFormType = IntakeFormType.AntiFungalRxOnly;
    intake.questions = [question1, question2, question3];

    return intake;
  }
}
