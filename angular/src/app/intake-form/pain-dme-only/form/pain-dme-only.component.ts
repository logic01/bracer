import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material';
import { ActivatedRoute } from '@angular/router';

import { Observable, Subject } from 'rxjs';

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
      rigthElbowQuestions: this.formBuilder.array([]),
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
    questions.push(this.formBuilder.group({ name: 'painFeeling', text: 'Cause of Patients Pain?' }));
    questions.push(this.formBuilder.group({ name: 'painBegan', text: 'Onset of pain (When did the pain begin?)' }));
    questions.push(this.formBuilder.group({ name: 'painCause', text: 'What Provokes Pain' }));
    questions.push(this.formBuilder.group({ name: 'painSelfTreatment', text: 'What currently relieves the pain?' }));
    questions.push(this.formBuilder.group({ name: 'painDescription', text: 'Description of Pain [Sharp/Stabbing, Weak Feeling/Unstable]' }));
    questions.push(this.formBuilder.group({ name: 'painDuration', text: 'Duration of Pain (Constant (Daily), Intermittent (from time to time), Specifically when (activity that makes it worse))' }));
    questions.push(this.formBuilder.group({ name: 'previousTreatment', text: 'Other or Previous Helpful Treatments(Brace, Physical Therapy, Meds)' }));
    questions.push(this.formBuilder.group({ name: 'effectsDaily', text: 'Affects Activities of Daily Living(ADL) (If so, what?)' }));
    questions.push(this.formBuilder.group({ name: 'hadSurgery', text: 'Have you had surgery in this area?' }));
    questions.push(this.formBuilder.group({ name: 'surgies', text: 'If yes, what type of surgery?' }));
    questions.push(this.formBuilder.group({ name: 'dateOfSurgery', text: 'Date of Surgery' }));
    questions.push(this.formBuilder.group({ name: 'abulatory', text: 'Are you abulatory? (can you walk on your own, or with a walker, or with a crutch)' }));
    questions.push(this.formBuilder.group({ name: 'painLevel', text: 'Pain Rating' }));

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
    const painPointIds: number[] = [];

    // For the checkboxes that are checked select
    // which array from the painPointQuestions need to be
    // added as an intake form
    /*if (this.LeftWrist) { painPointIds.push(0); }
    if (this.RightWrist) { painPointIds.push(1); }
    if (this.LeftElbow) { painPointIds.push(2); }
    if (this.RightElbow) { painPointIds.push(3); }
    if (this.LeftAnteriorShoulder) { painPointIds.push(4); }
    if (this.RightAnteriorShoulder) { painPointIds.push(5); }
    if (this.LeftKnee) { painPointIds.push(6); }
    if (this.RightKnee) { painPointIds.push(7); }
    if (this.LeftAnkle) { painPointIds.push(8); }
    if (this.RightAnkle) { painPointIds.push(9); }

    for (let pi = 0; pi < painPointIds.length; pi++) {
      const painPointId = painPointIds[pi];
      const intake = new IntakeForm();
      intake.patientId = this.patientId;
      intake.questions = [];
      intake.intakeFormType = IntakeFormType.PainDmeOnly;

      const painPointQuestions = this.painQuestions[painPointId];
      for (let i = 0; i < painPointQuestions.length; i++) {
        const painPointQuestion = painPointQuestions[i];
        let answerText = this.form.controls[painPointQuestion.getId()].value;
        if (painPointQuestion.elementId === '2') {
          answerText = painPointQuestion.painPointText;
        }
        intake.questions.push(this.addAnswer(painPointQuestion, answerText));
      }


      intake.status = IntakeStatus.New;
      intakeForms.push(intake);
    }


    // need to return each set of PainQuestion/Answers as individual intake forms
    return intakeForms;*/

  }


}



