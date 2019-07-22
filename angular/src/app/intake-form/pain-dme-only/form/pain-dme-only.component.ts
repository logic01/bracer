import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { Answer } from 'src/app/models/answer.model';
import { IntakeFormType } from 'src/app/models/enums/intake-form-type.enum';
import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Question } from 'src/app/models/question.model';
import { MaskService } from 'src/app/services/mask.service';
import { SelectValueService } from 'src/app/services/select-value.service';


export class PainQuestion {
  key: string;
  painPoint: string;
  text: string;
}

@Component({
  selector: 'app-pain-dme-only',
  templateUrl: './pain-dme-only.component.html',
  styleUrls: ['./pain-dme-only.component.scss']
})
export class PainDmeOnlyComponent implements OnInit, OnDestroy {

  @Output() formSubmitEvent = new EventEmitter<IntakeForm[]>();

  public form: FormGroup;
  public painPoints = SelectValueService.painPoints;

  public patientId: string;

  private unsubscribe$ = new Subject();

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    public readonly maskService: MaskService) { }

  ngOnInit() {
    this.patientId = this.route.snapshot.paramMap.get('patientId');
    this.initForm();
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  onCheck(event: MatCheckboxChange) {

    // https://alligator.io/angular/reactive-forms-formarray-dynamic-fields/

    const checked = event.checked;
    const painPoint = event.source.value;

    if (checked) {
      this.addQuestions(painPoint);
    } else {
      this.removeQuestions(painPoint);
    }

  }

  onSubmit() {

    if (!this.form.valid) {
      return;
    }

    const intakeForms = this.buildIntakeForms();

    this.formSubmitEvent.emit(intakeForms);

  }

  formArrayControls(key: string): AbstractControl[] {
    return (<FormArray>this.form.get(key)).controls;
  }

  private initForm() {

    this.form = this.formBuilder.group({
      leftElbow: new FormControl(''),
      leftElbowQuestions: this.formBuilder.array([]),
      rightElbow: new FormControl(''),
      rightElbowQuestions: this.formBuilder.array([]),
      leftAnteriorShoulder: new FormControl(''),
      leftAnteriorShoulderQuestions: this.formBuilder.array([]),
      rightAnteriorShoulder: new FormControl(''),
      rightAnteriorShoulderQuestions: this.formBuilder.array([]),
      leftPosteriorShoulder: new FormControl(''),
      leftPosteriorShoulderQuestions: this.formBuilder.array([]),
      rightPosteriorShoulder: new FormControl(''),
      rightPosteriorShoulderQuestions: this.formBuilder.array([]),
      leftKnee: new FormControl(''),
      leftKneeQuestions: this.formBuilder.array([]),
      rightKnee: new FormControl(''),
      rightKneeQuestions: this.formBuilder.array([]),
      leftAnkle: new FormControl(''),
      leftAnkleQuestions: this.formBuilder.array([]),
      rightAnkle: new FormControl(''),
      rightAnkleQuestions: this.formBuilder.array([])
    });
  }

  private buildIntakeForms() {

    const intakeForms: IntakeForm[] = [];

    this.addIntake(intakeForms, 'leftElbow');
    this.addIntake(intakeForms, 'rightElbow');

    return intakeForms;
  }

  private addIntake(intakeForms: IntakeForm[], key: string) {

    if (this.form.get(key).value === true) {
      intakeForms.push(this.buildIntake(key));
    }
  }

  private buildIntake(painPoint: string): IntakeForm {

    const formArray = this.form.get(painPoint + 'Questions') as FormArray;
    if (!formArray) {
      return;
    }

    const formGroup = formArray.controls[0] as FormGroup;
    if (!formGroup) {
      return;
    }

    const intake = new IntakeForm();
    intake.status = IntakeStatus.New;
    intake.patientId = this.patientId;
    intake.intakeFormType = IntakeFormType.PainDmeOnly;
    intake.questions = [];

    intake.questions.push(this.buildQuestion(formGroup, 'painFeeling'));
    intake.questions.push(this.buildQuestion(formGroup, 'painBegan'));
    intake.questions.push(this.buildQuestion(formGroup, 'painCause'));
    intake.questions.push(this.buildQuestion(formGroup, 'painSelfTreatment'));
    intake.questions.push(this.buildQuestion(formGroup, 'painDescription'));
    intake.questions.push(this.buildQuestion(formGroup, 'painDuration'));
    intake.questions.push(this.buildQuestion(formGroup, 'previousTreatment'));
    intake.questions.push(this.buildQuestion(formGroup, 'effectsDaily'));
    intake.questions.push(this.buildQuestion(formGroup, 'hadSurgery'));
    intake.questions.push(this.buildQuestion(formGroup, 'surgies'));
    intake.questions.push(this.buildQuestion(formGroup, 'dateOfSurgery'));
    intake.questions.push(this.buildQuestion(formGroup, 'abulatory'));
    intake.questions.push(this.buildQuestion(formGroup, 'painLevel'));

    return intake;
  }

  private buildQuestion(formGroup: FormGroup, key: string): Question {

    const question = new Question();
    question.key = key;
    question.text = key;
    question.answers = [];

    const answer = new Answer();
    answer.text = formGroup.get(key).value ? formGroup.get(key).value : '';
    question.answers.push(answer);

    return question;
  }

  private addQuestions(painPoint: string) {

    const questions = this.form.get(painPoint + 'Questions') as FormArray;
    const group = this.formBuilder.group({
      painFeeling: '', painBegan: '', painCause: '', painSelfTreatment: '',
      painDescription: '', painDuration: '', previousTreatment: '', effectsDaily: '',
      hadSurgery: '', surgies: '', dateOfSurgery: '', abulatory: '', painLevel: '',
    });

    questions.push(group);
  }

  private removeQuestions(painPoint: string) {
    const questions = this.form.get(painPoint + 'Questions') as FormArray;
    for (let i = 0; i < questions.length; i++) {
      questions.removeAt(i);
    }
  }


}
