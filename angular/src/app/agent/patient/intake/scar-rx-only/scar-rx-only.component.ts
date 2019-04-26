import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatRadioChange } from '@angular/material';
import { ActivatedRoute } from '@angular/router';

import { Answer } from 'src/app/models/answer.model';
import { IntakeFormType } from 'src/app/models/enums/intake-form-type.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Question } from 'src/app/models/question.model';

@Component({
  selector: 'app-scar-rx-only',
  templateUrl: './scar-rx-only.component.html',
  styleUrls: ['./scar-rx-only.component.scss']
})
export class ScarRxOnlyComponent implements OnInit {

  @Output() formSubmitEvent = new EventEmitter<IntakeForm>();

  form: FormGroup;
  patientId: string;
  questions: Question[] = [];
  q3DisplayOther = false;

  constructor(private readonly route: ActivatedRoute) { }

  ngOnInit() {
    this.patientId = this.route.snapshot.paramMap.get('id');

    this.initQuestions();
    this.initForm();
  }

  initQuestions() {
    const question1 = new Question();
    question1.key = 'q1';
    question1.text = '1. Where is your scar located?';

    const question2 = new Question();
    question2.key = 'q2';
    question2.text = '2. How long have you had this scar?';

    const question3 = new Question();
    question3.key = 'q3';
    question3.text = '3. What is the cause of your scar?';

    const question4 = new Question();
    question4.key = 'q4';
    question4.text = '4. Have you treated the scar before?';

    this.questions.push(question1, question2, question3, question4);
  }

  initForm() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q2: new FormControl('', Validators.required),
      q3: new FormControl(''),
      q3_other: new FormControl(''),
      q4_1: new FormControl(''),
      q4_2: new FormControl(''),
      q4_3: new FormControl(''),
      q4_4: new FormControl(''),
      q4_5: new FormControl('')
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

    if (answer3.text === 'other') {
      answer3.text = this.form.controls['q3_other'].value;
    }

    this.questions[2].answers = [answer3];

    const answer4_1 = new Answer();
    answer4_1.text = this.form.controls['q4_1'].value;

    const answer4_2 = new Answer();
    answer4_2.text = this.form.controls['q4_2'].value;

    const answer4_3 = new Answer();
    answer4_3.text = this.form.controls['q4_3'].value;

    const answer4_4 = new Answer();
    answer4_4.text = this.form.controls['q4_4'].value;

    this.questions[3].answers = [answer4_1, answer4_2, answer4_3, answer4_4];

    const intake = new IntakeForm();
    intake.intakeFormType = IntakeFormType.AntiFungalRxOnly;
    intake.questions = this.questions;
    intake.patientId = this.patientId;

    return intake;
  }

  q3_change($event: MatRadioChange) {
    if ($event.source.name === 'other') {
      this.q3DisplayOther = !this.q3DisplayOther;
    }
  }

}
