import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { SelectValueService } from 'src/app/services/select-value.service';
import { Question, PainQuestion } from 'src/app/models/question.model';
import { CustomValidators } from 'src/app/validators/custom-validators';
import { Answer } from 'src/app/models/answer.model';
import { IntakeFormType } from 'src/app/models/enums/intake-form-type.enum';
import { MaskService } from 'src/app/services/mask.service'

@Component({
  selector: 'app-pain-dme-only',
  templateUrl: './pain-dme-only.component.html',
  styleUrls: ['./pain-dme-only.component.scss']
})
export class PainDmeOnlyComponent implements OnInit {

  @Output() formSubmitEvent = new EventEmitter<IntakeForm>();


  LeftWrist: boolean = false;
  RightWrist: boolean = false;
  LeftElbow: boolean = false;
  RightElbow: boolean = false;
  LeftAnteriorShoulder: boolean = false;
  RightAnteriorShoulder: boolean = false;
  LeftHip: boolean = false;
  RightHip: boolean = false;
  LeftKnee: boolean = false;
  RightKnee: boolean = false;
  PosteriorLeftShoulder: boolean = false;
  PosteriorRightShoulder: boolean = false;
  Neck: boolean = false;
  UpperMiddleBack: boolean = false;
  MiddleBack: boolean = false;
  LowerBack: boolean = false;
  LeftOccipital: boolean = false;
  RightOccipital: boolean = false;
  FrontalorForehead: boolean = false;
  LeftAnkle: boolean = false;
  RightAnkle: boolean = false;


  painQuestions: PainQuestion[][];
  painPoints = SelectValueService.painPoints;
  form: FormGroup;
  patientId: string;

  constructor(private readonly route: ActivatedRoute, public readonly maskService: MaskService) { }


  ngOnInit() {
    this.patientId = this.route.snapshot.paramMap.get('id');

    this.initQuestions();
    this.initForm();
  }

  initForm() {
    //dynamically adding the formGroups by the pain question possiblities
    this.form = new FormGroup({});
    for (let i = 0; i < this.painQuestions.length; i++) {
      let painQuestionSet: PainQuestion[] = this.painQuestions[i];
      let checkBoxId = i + '_check';
      this.form.addControl(checkBoxId, new FormControl());
      for (let p = 0; p < painQuestionSet.length; p++) {
        let painQuestion = painQuestionSet[p];
        this.form.addControl(painQuestion.getId(), new FormControl());
      };
    };
  }

  onSubmit() {

    if (!this.form.valid) {
      return;
    }

    const intakeForms = this.buildIntakeForms();

    //TODO submit ALL of the intake forms and not just the first one
    this.formSubmitEvent.emit(intakeForms[0]);
  }

  initQuestions() {
    this.painQuestions = [];
    for (let painPoint in this.painPoints) {
      let painArray: PainQuestion[] = [];

      painArray.push(this.initPainQuestion('PainFeeling', 'Cause of Patients Pain?', painPoint, 1));
      painArray.push(this.initPainQuestion('PainChart', 'Location(s) of Pain?', painPoint, 2));
      painArray.push(this.initPainQuestion('PainBegin', 'Onset of pain (When did the pain begin?)', painPoint, 3));
      painArray.push(this.initPainQuestion('PainCause', 'What Provokes Pain', painPoint, 4));
      painArray.push(this.initPainQuestion('PainSelfTreatment', 'What currently relieves the pain', painPoint, 5));
      painArray.push(this.initPainQuestion('PainDescription', 'Description of Pain [Sharp/Stabbing, Weak Feeling/Unstable]', painPoint, 6));
      painArray.push(this.initPainQuestion('PainDuration', 'Duration of Pain (Constant (Daily), Intermittent (from time to time), Specifically when (activity that makes it worse))', painPoint, 7));
      painArray.push(this.initPainQuestion('PreviousTreatment', 'Other or Previous Helpful Treatments(Brace, Physical Therapy, Meds)', painPoint, 8));
      painArray.push(this.initPainQuestion('EffectsDaily', 'Affects Activities of Daily Living(ADL) (If so, what?)', painPoint, 9));
      painArray.push(this.initPainQuestion('10', 'Have you had surgery in this area?', painPoint, 10));
      painArray.push(this.initPainQuestion('Surgies', 'If yes, what type of surgery?', painPoint, 11));
      painArray.push(this.initPainQuestion('12', 'Date of Surgery', painPoint, 12));
      painArray.push(this.initPainQuestion('13', 'Is this a request for a TENS UNIT', painPoint, 13));
      painArray.push(this.initPainQuestion('14', 'Are you able to ambulate? (can you walk on your own, or with a walker, or with a crutch)', painPoint, 14));
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

    let intakeForms: IntakeForm[] = [];
    let painPointIds: number[] = [];

    //For the checkboxes that are checked select
    //which array from the painPointQuestions need to be
    //added as an intake form
    if (this.LeftWrist) painPointIds.push(0);
    if (this.RightWrist) painPointIds.push(1);
    if (this.LeftElbow) painPointIds.push(2);
    if (this.RightElbow) painPointIds.push(3);
    if (this.LeftAnteriorShoulder) painPointIds.push(4);
    if (this.RightAnteriorShoulder) painPointIds.push(5);
    if (this.LeftHip) painPointIds.push(6);
    if (this.RightHip) painPointIds.push(7);
    if (this.LeftKnee) painPointIds.push(8);
    if (this.RightKnee) painPointIds.push(9);
    if (this.PosteriorLeftShoulder) painPointIds.push(10);
    if (this.PosteriorRightShoulder) painPointIds.push(11);
    if (this.Neck) painPointIds.push(12);
    if (this.UpperMiddleBack) painPointIds.push(13);
    if (this.MiddleBack) painPointIds.push(14);
    if (this.LowerBack) painPointIds.push(15);
    if (this.LeftOccipital) painPointIds.push(16);
    if (this.RightOccipital) painPointIds.push(17);
    if (this.FrontalorForehead) painPointIds.push(18);
    if (this.LeftAnkle) painPointIds.push(19);
    if (this.RightAnkle) painPointIds.push(20);


    for (let pi = 0; pi < painPointIds.length; pi++) {
      const painPointId = painPointIds[pi];
      let intake = new IntakeForm();
      intake.patientId = this.patientId;
      intake.questions = [];
      intake.intakeFormType = IntakeFormType.PainDmeOnly;


      const painPointQuestions = this.painQuestions[painPointId];
      for (let i = 0; i < painPointQuestions.length; i++) {
        const painPointQuestion = painPointQuestions[i];
        let answerText = this.form.controls[painPointQuestion.getId()].value;
        if(painPointQuestion.elementId === '2'){
          answerText = painPointQuestion.painPointText;
        }
        intake.questions.push(this.addAnswer(painPointQuestion, answerText));
      }
      intakeForms.push(intake);
    }


    //need to return each set of PainQuestion/Answers as individual intake forms
    return intakeForms;

  }

  addAnswer(question: PainQuestion, value: string): Question {

    const answer = new Answer();
    answer.text = value;
    question.answers = [];
    question.answers.push(answer);
    return question;
  }

  getPainCheckboxBinding(painPointText: string) {
    //On the UI this will set the value of the dynamic Pain DME Intake Forms
    //with the proper model boolean that maps to the checkbox
    if (painPointText === 'LeftWrist') return this.LeftWrist;
    else if (painPointText === 'RightWrist') return this.RightWrist;
    else if (painPointText === 'LeftElbow') return this.LeftElbow;
    else if (painPointText === 'RightElbow') return this.RightElbow;
    else if (painPointText === 'LeftAnteriorShoulder') return this.LeftAnteriorShoulder;
    else if (painPointText === 'RightAnteriorShoulder') return this.RightAnteriorShoulder;
    else if (painPointText === 'LeftHip') return this.LeftHip;
    else if (painPointText === 'RightHip') return this.RightHip;
    else if (painPointText === 'LeftKnee') return this.LeftKnee;
    else if (painPointText === 'RightKnee') return this.RightKnee;
    else if (painPointText === 'PosteriorLeftShoulder') return this.PosteriorLeftShoulder;
    else if (painPointText === 'PosteriorRightShoulder') return this.PosteriorRightShoulder;
    else if (painPointText === 'Neck') return this.Neck;
    else if (painPointText === 'UpperMiddleBack') return this.UpperMiddleBack;
    else if (painPointText === 'MiddleBack') return this.MiddleBack;
    else if (painPointText === 'LowerBack') return this.LowerBack;
    else if (painPointText === 'LeftOccipital') return this.LeftOccipital;
    else if (painPointText === 'RightOccipital') return this.RightOccipital;
    else if (painPointText === 'FrontalorForehead') return this.FrontalorForehead;
    else if (painPointText === 'LeftAnkle') return this.LeftAnkle;
    return this.RightAnkle
  }

}



