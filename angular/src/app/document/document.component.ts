import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material';
import { forkJoin, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { IntakeForm } from '../models/intake-form.model';
import { Patient } from '../models/patient.model';
import { Physician } from '../models/physician.model';
import { IntakeFormService } from '../services/api/intake-form.service';
import { PatientService } from '../services/api/patient.service';
import { PhysicianService } from '../services/api/physician.service';

export interface ICD10 {
  text: string;
}

export interface HCPCS {
  product: string;
  text: string;
}

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements OnInit {

  public form: FormGroup;

  @Input() patient$: Observable<Patient> = this.patientApi.get('1');
  @Input() physician$: Observable<Physician> = this.physicianApi.get('6');
  @Input() intakeForm$: Observable<IntakeForm> = this.intakeFormApi.get('1');

  public data$ = forkJoin(this.patient$, this.physician$, this.intakeForm$).pipe(
    map(([patient, physician, intake]) => {
      return { patient, physician, intake };
    }));

  public product = 'Ankle Brace';
  public painArea = 'Right Ankle';
  public signatureDate = Date.now();
  public dateOfService = Date.now();
  public diagnosisOptions: ICD10[] = [];
  public diagnosis: ICD10[] = [];
  public lcodeOptions: HCPCS[] = [];
  public lcodeText: HCPCS[] = [];

  constructor(
    private readonly patientApi: PatientService,
    private readonly physicianApi: PhysicianService,
    private readonly intakeFormApi: IntakeFormService) {
  }

  ngOnInit() {

    this.form = new FormGroup({
      diagnosis_other: new FormControl(''),
      lcode_other: new FormControl(''),
      additional_notes: new FormControl(''),
      lengthOfNeed: new FormControl(''),
    });

    this.setDiagnosis(this.painArea);
  }

  private setDiagnosis(key: string) {

    switch (key.toUpperCase()) {
      case 'RIGHT ANKLE':
        this.diagnosisOptions.push({ text: 'm19.071 primary osteoarthritis, right ankle and foot' });
        this.diagnosisOptions.push({ text: 'm25.579 pain in joint, ankle' });
        this.diagnosisOptions.push({ text: 'm25.571 pain in right ankle and joints of right foot' });
        this.diagnosisOptions.push({ text: 'm25.371 instability in right ankle' });
        this.lcodeOptions.push({ product: 'product', text: 'L1906 Ankle Foot Orthosis, Plastic or Other Material With Ankle Joint, Prefabricated, Includes Fitting and Adjustment' });
        break;
      case 'LEFT ANKLE':
        this.diagnosisOptions.push({ text: 'm19.072 primary osteoarthritis, left ankle and foot' });
        this.diagnosisOptions.push({ text: 'm25.579 pain in joint, ankle' });
        this.diagnosisOptions.push({ text: 'm25.572 pain in left ankle and joints of left foot' });
        this.diagnosisOptions.push({ text: 'm25.372 instability left ankle' });
        this.lcodeOptions.push({ product: 'product', text: 'L1906 Ankle Foot Orthosis, Plastic or Other Material With Ankle Joint, Prefabricated, Includes Fitting and Adjustment' });
        break;
      case 'RIGHT KNEE ':
        this.diagnosisOptions.push({ text: 'm17.11 unilateral osteoarthritis of the right knee' });
        this.diagnosisOptions.push({ text: 'm23.51 chronic instability of the right knee' });
        this.lcodeOptions.push({ product: 'product', text: 'L1833 (Hinged Wraparound Knee Support)' });
        this.lcodeOptions.push({ product: 'product', text: 'L2397 (Universal Suspension Sleeve for ROM Hinged Knee Braces)' });
        break;
      case 'LEFT KNEE':
        this.diagnosisOptions.push({ text: 'm17.12 unilateral osteoarthritis of left knee' });
        this.diagnosisOptions.push({ text: 'm23.52 chronic instability of the left knee' });
        this.lcodeOptions.push({ product: 'product', text: 'L1833 (Hinged Wraparound Knee Support)' });
        this.lcodeOptions.push({ product: 'product', text: 'L2397 (Universal Suspension Sleeve for ROM Hinged Knee Braces)' });
        break;
      case 'RIGHT WRIST':
        this.diagnosisOptions.push({ text: 'm19.031 primary osteoarthritis, right wrist' });
        this.diagnosisOptions.push({ text: 'g56.0 carpal tunnel syndrome' });
        this.diagnosisOptions.push({ text: 'm19.041 osteoarthritis right hand' });
        this.lcodeOptions.push({ product: 'product', text: 'L3908 wrist hand orthosis, includes one or more non-torsional joint(s), elastic bands, turnbuckles, may include a soft interface and straps. it is prefabricated and off the shelf.' });
        break;
      case 'LEFT WRIST':
        this.diagnosisOptions.push({ text: 'm19.032 primary osteoarthritis, left wrist' });
        this.diagnosisOptions.push({ text: 'g56.0 carpal tunnel syndrome' });
        this.diagnosisOptions.push({ text: 'm19.042 osteoarthritis left hand' });
        this.lcodeOptions.push({ product: 'product', text: 'L3908 wrist hand orthosis, includes one or more non-torsional joint(s), elastic bands, turnbuckles, may include a soft interface and straps. it is prefabricated and off the shelf.' });
        break;
      case 'RIGHT ELBOW':
        this.diagnosisOptions.push({ text: 'm24.221 disorder of ligament, right elbow' });
        this.diagnosisOptions.push({ text: 'm12.821 other specific arthropathy, not elsewhere specified, right elbow' });
        this.diagnosisOptions.push({ text: 'm12.821 pain in right elbow' });
        this.diagnosisOptions.push({ text: 'g89.4 chronic pain' });
        this.lcodeOptions.push({ product: 'product', text: 'L3761 Elbow Orthosis, With Adjustable Position Locking Joint(s), Prefabricated, Includes Fitting and Adjustments, Any Type.' });
        break;
      case 'LEFT ELBOW':
        this.diagnosisOptions.push({ text: 'm24.221 disorder of ligament, left  elbow' });
        this.diagnosisOptions.push({ text: 'm12.821 other specific arthropathy, not elsewhere specified, left elbow' });
        this.diagnosisOptions.push({ text: 'm25.521 pain in left elbow' });
        this.diagnosisOptions.push({ text: 'g89.4 chronic pain' });
        this.lcodeOptions.push({ product: 'product', text: 'L3761 Elbow Orthosis, With Adjustable Position Locking Joint(s), Prefabricated, Includes Fitting and Adjustments, Any Type.' });
        break;
      case 'RIGHT SHOULDER':
        this.diagnosisOptions.push({ text: 'm25.511 pain in right shoulder' });
        this.diagnosisOptions.push({ text: 'm19.011 primary osteoarthritis, right shoulder' });
        this.lcodeOptions.push({ product: 'product', text: 'L3960 (Shoulder Elbow Wrist Hand Orthosis, Abduction Positioning, Airplane Design, Prefabricated, Includes Fitting and Adjustment.)' });
        break;
      case 'LEFT SHOULDER':
        this.diagnosisOptions.push({ text: 'm25.512 pain in left shoulder ' });
        this.diagnosisOptions.push({ text: 'm19.012 primary osteoarthritis, left shoulder' });
        this.lcodeOptions.push({ product: 'product', text: 'L3960(Shoulder Elbow Wrist Hand Orthosis, Abduction Positioning, Airplane Design, Prefabricated, Includes Fitting and Adjustment.)' });
        break;
      case 'LOWER BACK':
        this.diagnosisOptions.push({ text: 'm54.5 low back pain' });
        this.diagnosisOptions.push({ text: 'm53.2x7 spinal instabilities, lumbosacral region' });
        this.diagnosisOptions.push({ text: 'g89.4 chronic pain' });
        this.diagnosisOptions.push({ text: 'm51.36 lumbar disc degeneration' });
        this.lcodeOptions.push({ product: 'product', text: 'L0650 (Lumbar-sacral orthosis. Sagittal control with rigid anterior and posterior panels, posterior panels, posterior extends from Sacrococcygeal junction to the T-9 vertebra, lateral strength, with rigid lateral panels, prefabricated and off the shelf. Custom fitting of the orthosis is not required and the patient or an assisting care giver can apply the prescribed orthotic device with minimal self-adjusting.)' });
        break;

    }
  }

  onDiagnosisCheck(event: MatCheckboxChange, index: number) {
    if (event.checked) {
      this.diagnosis.push(this.diagnosisOptions[index]);
    } else {
      this.diagnosis = this.diagnosis.filter(obj => obj !== this.diagnosisOptions[index]);
    }
  }

  onLCodeCheck(event: MatCheckboxChange, index: number) {

    if (event.checked) {
      this.lcodeText.push(this.lcodeOptions[index]);
    } else {
      this.lcodeText = this.lcodeText.filter(obj => obj !== this.lcodeOptions[index]);
    }
  }
}
