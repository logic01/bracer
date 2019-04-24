import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

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
  questions: Question[] = [];
  patientId: string;

  constructor(private readonly route: ActivatedRoute) { }

  ngOnInit() {
    this.patientId = this.route.snapshot.paramMap.get('id');

    this.initQuestions();
    this.initForm();
  }

  initQuestions() {
    const question1 = new Question();
    question1.key = 'q1';
    question1.text = '1. Where is your skin issue located?';

    const question2 = new Question();
    question2.key = 'q2';
    question2.text = '2. How long have you had this skin issue?';

    const question3 = new Question();
    question3.key = 'q3';
    question3.text = '3. Have you treated the skin irritation before?';

    this.questions.push(question1, question2, question3);
  }

  initForm() {
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

    const answer1 = new Answer();
    answer1.text = this.form.controls['q1'].value;
    this.questions[0].answers = [answer1];

    const answer2 = new Answer();
    answer2.text = this.form.controls['q2'].value;
    this.questions[1].answers = [answer2];

    const answer3 = new Answer();
    answer3.text = this.form.controls['q3'].value;
    this.questions[2].answers = [answer3];

    const intake = new IntakeForm();
    intake.intakeFormType = IntakeFormType.AntiFungalRxOnly;
    intake.questions = this.questions;
    intake.patientId = this.patientId;

    return intake;
  }

}
