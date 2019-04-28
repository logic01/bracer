import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Question } from 'src/app/models/question.model';
import { ActivatedRoute } from '@angular/router';
import { IntakeFormType } from 'src/app/models/enums/intake-form-type.enum';
import { Answer } from 'src/app/models/answer.model';

@Component({
  selector: 'app-general-dme-only',
  templateUrl: './general-dme-only.component.html',
  styleUrls: ['./general-dme-only.component.scss']
})
export class GeneralDmeOnlyComponent implements OnInit {

  @Output() formSubmitEvent = new EventEmitter<IntakeForm>();

  form: FormGroup;
  patientId: string;
  questions: Question[] = [];

  constructor(private readonly route: ActivatedRoute) { }

  ngOnInit() {
    this.patientId = this.route.snapshot.paramMap.get('id');

    this.initQuestions();
    this.initForm();
  }

  initForm() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q2: new FormControl('', Validators.required),
      q3: new FormControl('', Validators.required),
      q4: new FormControl('', Validators.required),
      q5: new FormControl('', Validators.required),
      q6: new FormControl('', Validators.required),
      q7: new FormControl('', Validators.required),
      q8: new FormControl('', Validators.required),
      q9: new FormControl('', Validators.required),
      q10: new FormControl('', Validators.required)
    });
  }

  initQuestions() {
    this.questions.push(this.initQuestion('1', 'How tall are you?'));
    this.questions.push(this.initQuestion('2', 'How much do you weigh?'));
    this.questions.push(this.initQuestion('3', 'What is your shoe size?'));
    this.questions.push(this.initQuestion('4', 'What is your waist size?'));
    this.questions.push(this.initQuestion('5', 'What current medications are you taking?'));
    this.questions.push(this.initQuestion('6', 'Do you have any allergies?'));
    this.questions.push(this.initQuestion('7', 'Have you seen your primary care physician within the last year?'));
    this.questions.push(this.initQuestion('8', 'Have you been prescribed any type of brace within the last 5 years?'));
    this.questions.push(this.initQuestion('9', 'Are you diabetic?'));
    this.questions.push(this.initQuestion('10', 'Do you take insulin or oral medication for diabetes?'));
  }

  initQuestion(key: string, text: string): Question {
    const question = new Question();
    question.key = key;
    question.text = text;
    question.answers = [];

    return question;
  }

  onSubmit() {

    if (!this.form.valid) {
      return;
    }

    const intakeForm = this.buildIntake();

    this.formSubmitEvent.emit(intakeForm);
  }

  buildIntake(): IntakeForm {

    this.addAnswer(this.questions[0], this.form.controls['q1'].value);
    this.addAnswer(this.questions[1], this.form.controls['q2'].value);
    this.addAnswer(this.questions[2], this.form.controls['q3'].value);
    this.addAnswer(this.questions[3], this.form.controls['q4'].value);
    this.addAnswer(this.questions[4], this.form.controls['q5'].value);
    this.addAnswer(this.questions[5], this.form.controls['q6'].value);
    this.addAnswer(this.questions[6], this.form.controls['q7'].value);
    this.addAnswer(this.questions[7], this.form.controls['q8'].value);
    this.addAnswer(this.questions[8], this.form.controls['q9'].value);
    this.addAnswer(this.questions[9], this.form.controls['q10'].value);

    const intake = new IntakeForm();
    intake.intakeFormType = IntakeFormType.GeneralDmeOnly;
    intake.questions = this.questions;
    intake.patientId = this.patientId;

    return intake;
  }

  addAnswer(question: Question, value: string) {
    const answer = new Answer();
    answer.text = value;
    question.answers.push(answer);
  }
}
