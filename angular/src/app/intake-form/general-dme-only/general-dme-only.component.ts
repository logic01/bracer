import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Answer } from 'src/app/models/answer.model';
import { IntakeFormType } from 'src/app/models/enums/intake-form-type.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Question } from 'src/app/models/question.model';
import { SelectValueService } from 'src/app/services/select-value.service';
import { CustomValidators } from 'src/app/validators/custom-validators';

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
  shoeSizes: string[] = SelectValueService.shoeSizes;
  heights: string[] = SelectValueService.heights;

  constructor(private readonly route: ActivatedRoute) { }

  ngOnInit() {
    this.patientId = this.route.snapshot.paramMap.get('id');

    this.initQuestions();
    this.initForm();
  }

  initForm() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q2: new FormControl('', [Validators.required, CustomValidators.onlyNumeric]),
      q3: new FormControl('', Validators.required),
      q4: new FormControl('', [Validators.required, CustomValidators.onlyNumeric]),
      q5: new FormControl('', Validators.required),
      q6: new FormControl('', Validators.required),
      q7: new FormControl('', Validators.required),
      q8: new FormControl('', Validators.required),
      q9: new FormControl('', Validators.required),
      q10: new FormControl('', Validators.required)
    });
  }

  initQuestions() {
    this.questions.push(this.initQuestion('Height', 'How tall are you?'));
    this.questions.push(this.initQuestion('Weight', 'How much do you weigh?'));
    this.questions.push(this.initQuestion('ShoeSize', 'What is your shoe size?'));
    this.questions.push(this.initQuestion('Waist', 'What is your waist size?'));
    this.questions.push(this.initQuestion('5', 'What current medications are you taking?'));
    this.questions.push(this.initQuestion('Allergies', 'Do you have any allergies?'));
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

    this.addAnswer(this.questions[0], this.getControlValue('q1'));
    this.addAnswer(this.questions[1], this.getControlValue('q2'));
    this.addAnswer(this.questions[2], this.getControlValue('q3'));
    this.addAnswer(this.questions[3], this.getControlValue('q4') + ' inches');
    this.addAnswer(this.questions[4], this.getControlValue('q5'));
    this.addAnswer(this.questions[5], this.getControlValue('q6'));
    this.addAnswer(this.questions[6], this.getControlValue('q7'));
    this.addAnswer(this.questions[7], this.getControlValue('q8'));
    this.addAnswer(this.questions[8], this.getControlValue('q9'));
    this.addAnswer(this.questions[9], this.getControlValue('q10'));

    const intake = new IntakeForm();
    intake.intakeFormType = IntakeFormType.GeneralDmeOnly;
    intake.questions = this.questions;
    intake.patientId = this.patientId;

    return intake;
  }

  getControlValue(key: string) {
    const val = this.form.controls[key].value;

    if (val) {
      return val;
    }

    return '';
  }

  addAnswer(question: Question, value: string) {
    const answer = new Answer();
    answer.text = value;
    question.answers.push(answer);
  }
}
