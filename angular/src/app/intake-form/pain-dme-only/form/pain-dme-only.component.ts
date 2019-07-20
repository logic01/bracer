import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { IntakeFormType } from 'src/app/models/enums/intake-form-type.enum';
import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
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

  @Input() intake$: Observable<IntakeForm>;
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

  initForm() {

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

  onSubmit() {

    if (!this.form.valid) {
      return;
    }

    const intakeForms = this.buildIntakeForms();

    this.formSubmitEvent.emit();

  }

  buildIntakeForms() {

    const intakeForms: IntakeForm[] = [];

    this.addIntake(intakeForms, 'leftElbow');

    return intakeForms;

  }

  addIntake(intakeForms: IntakeForm[], painPoint: string) {


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



    const questions1 = formGroup.get('painFeeling');
    const questions2 = formGroup.get('painBegan');
    const questions3 = formGroup.get('painCause');
    const questions4 = formGroup.get('painSelfTreatment');
    const questions5 = formGroup.get('painDescription');
    const questions6 = formGroup.get('painDuration');
    const questions7 = formGroup.get('previousTreatment');
    const questions13 = formGroup.get('effectsDaily');

    const questions8 = formGroup.get('hadSurgery');
    const questions9 = formGroup.get('surgies');
    const questions10 = formGroup.get('dateOfSurgery');
    const questions11 = formGroup.get('abulatory');
    const questions12 = formGroup.get('painLevel');


    /*  const question = new Question();
      question.answers = [];

      const answer = new Answer();
      answer.text = questions.controls[i].value ? questions.controls[i].value : '';
      question.answers.push(answer);
      question.key = Object.keys(this.form).find(name => questions.controls[i] === this.form[name]) || null;
      question.text = Object.keys(this.form).find(name => questions.controls[i] === this.form[name]) || null;*/


    intakeForms.push(intake);
  }

}
