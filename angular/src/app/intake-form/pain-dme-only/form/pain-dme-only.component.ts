import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Observable, Subject } from 'rxjs';

import { Answer } from 'src/app/models/answer.model';
import { IntakeFormType } from 'src/app/models/enums/intake-form-type.enum';
import { IntakeStatus } from 'src/app/models/enums/intake-status.enum';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { PainQuestion, Question } from 'src/app/models/question.model';
import { IntakeFormService } from 'src/app/services/api/intake-form.service';
import { MaskService } from 'src/app/services/mask.service';
import { SelectValueService } from 'src/app/services/select-value.service';

@Component({
  selector: 'app-pain-dme-only',
  templateUrl: './pain-dme-only.component.html',
  styleUrls: ['./pain-dme-only.component.scss']
})
export class PainDmeOnlyComponent implements OnInit, OnDestroy {

  @Input() intake$: Observable<IntakeForm>;
  @Output() formSubmitEvent = new EventEmitter<IntakeForm[]>();

  public form: FormGroup;

  public LeftWrist = false;
  public RightWrist = false;
  public LeftElbow = false;
  public RightElbow = false;
  public LeftAnteriorShoulder = false;
  public RightAnteriorShoulder = false;
  public LeftKnee = false;
  public RightKnee = false;
  public LeftPosteriorShoulder = false;
  public RightPosteriorShoulder = false;
  public LeftAnkle = false;
  public RightAnkle = false;

  public painQuestions: PainQuestion[][];
  public painPoints = SelectValueService.painPoints;

  public patientId: string;

  private unsubscribe$ = new Subject();

  constructor(
    private readonly route: ActivatedRoute,
    public readonly maskService: MaskService,
    private readonly intakeApi: IntakeFormService) { }

  ngOnInit() {
    this.patientId = this.route.snapshot.paramMap.get('patientId');

    this.initQuestions();
    this.initForm();
  }

  ngOnDestroy(): void {
    this.unsubscribe$.unsubscribe();
  }

  initForm() {
    // dynamically adding the formGroups by the pain question possiblities
    this.form = new FormGroup({});
    for (let i = 0; i < this.painQuestions.length; i++) {
      const painQuestionSet: PainQuestion[] = this.painQuestions[i];
      const checkBoxId = i + '_check';
      this.form.addControl(checkBoxId, new FormControl());
      for (let p = 0; p < painQuestionSet.length; p++) {
        const painQuestion = painQuestionSet[p];
        this.form.addControl(painQuestion.getId(), new FormControl());
      }
    }
  }

  onSubmit() {

    if (!this.form.valid) {
      return;
    }

    const intakeForms = this.buildIntakeForms();

    this.formSubmitEvent.emit(intakeForms);

  }

  initQuestions() {
    this.painQuestions = [];
    // tslint:disable-next-line: forin
    for (const painPoint in this.painPoints) {
      const painArray: PainQuestion[] = [];

      painArray.push(this.initPainQuestion('PainFeeling', 'Cause of Patients Pain?', painPoint, 1));
      painArray.push(this.initPainQuestion('PainChart', 'Location(s) of Pain?', painPoint, 2));
      painArray.push(this.initPainQuestion('PainBegan', 'Onset of pain (When did the pain begin?)', painPoint, 3));
      painArray.push(this.initPainQuestion('PainCause', 'What Provokes Pain', painPoint, 4));
      painArray.push(this.initPainQuestion('PainSelfTreatment', 'What currently relieves the pain', painPoint, 5));
      painArray.push(this.initPainQuestion('PainDescription', 'Description of Pain [Sharp/Stabbing, Weak Feeling/Unstable]', painPoint, 6));
      painArray.push(this.initPainQuestion('PainDuration', 'Duration of Pain (Constant (Daily), Intermittent (from time to time), Specifically when (activity that makes it worse))', painPoint, 7));
      painArray.push(this.initPainQuestion('PreviousTreatment', 'Other or Previous Helpful Treatments(Brace, Physical Therapy, Meds)', painPoint, 8));
      painArray.push(this.initPainQuestion('EffectsDaily', 'Affects Activities of Daily Living(ADL) (If so, what?)', painPoint, 9));
      painArray.push(this.initPainQuestion('HadSurgery', 'Have you had surgery in this area?', painPoint, 10));
      painArray.push(this.initPainQuestion('Surgies', 'If yes, what type of surgery?', painPoint, 11));
      painArray.push(this.initPainQuestion('DateOfSurgery', 'Date of Surgery', painPoint, 12));
      painArray.push(this.initPainQuestion('Abulatory', 'Are you abulatory? (can you walk on your own, or with a walker, or with a crutch)', painPoint, 14));
      painArray.push(this.initPainQuestion('PainLevel', 'Pain Rating', painPoint, 15));
      this.painQuestions.push(painArray);
    }
  }

  initPainQuestion(key: string, text: string, painPoint: string, elementId: number): PainQuestion {
    const question = new PainQuestion();
    question.key = key;
    question.text = text;
    question.answers = [];
    question.painPoint = painPoint;
    question.painPointText = this.painPoints[painPoint];
    question.elementId = elementId.toString();
    return question;
  }

  buildIntakeForms(): IntakeForm[] {

    const intakeForms: IntakeForm[] = [];
    const painPointIds: number[] = [];

    // For the checkboxes that are checked select
    // which array from the painPointQuestions need to be
    // added as an intake form
    if (this.LeftWrist) { painPointIds.push(0); }
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
    return intakeForms;

  }

  addAnswer(question: PainQuestion, value: string): Question {

    const answer = new Answer();
    answer.text = value ? value : '';
    question.answers = [];
    question.answers.push(answer);
    return question;
  }

  getPainCheckboxBinding(painPointText: string) {
    // On the UI this will set the value of the dynamic Pain DME Intake Forms
    // with the proper model boolean that maps to the checkbox

    let binding: boolean;

    switch (painPointText) {
      case 'LeftWrist':
        binding = this.LeftWrist;
        break;
      case 'RightWrist':
        binding = this.RightWrist;
        break;
      case 'LeftElbow':
        binding = this.LeftElbow;
        break;
      case 'RightElbow':
        binding = this.RightElbow;
        break;
      case 'LeftAnteriorShoulder':
        binding = this.LeftAnteriorShoulder;
        break;
      case 'RightAnteriorShoulder':
        binding = this.RightAnteriorShoulder;
        break;
      case 'RightAnteriorShoulder':
        binding = this.RightAnteriorShoulder;
        break;
      case 'LeftKnee':
        binding = this.LeftKnee;
        break;
      case 'RightKnee':
        binding = this.RightKnee;
        break;
      case 'LeftPosteriorShoulder':
        binding = this.LeftPosteriorShoulder;
        break;
      case 'RightPosteriorShoulder':
        binding = this.RightPosteriorShoulder;
        break;
      case 'LeftAnkle':
        binding = this.LeftAnkle;
        break;
      case 'RightAnkle':
        binding = this.RightAnkle;
        break;
      default: binding = false;
        break;
    }

    return binding;
  }

}



