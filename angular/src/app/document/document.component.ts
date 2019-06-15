import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUrls } from '../constants/routes';
import { AccountType } from '../models/enums/account-type.enum';
import { IntakeForm } from '../models/intake-form.model';
import { Patient } from '../models/patient.model';
import { Physician } from '../models/physician.model';
import { IntakeFormService } from '../services/api/intake-form.service';
import { PatientService } from '../services/api/patient.service';
import { PhysicianService } from '../services/api/physician.service';
import { SessionService } from '../services/session.service';
import { IntakeFormData } from './intake/intake.component';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements OnInit {

  private intakeFormId: string;
  private vendorId: string;

  public isAdminView = false;

  public patient: Patient;
  public physician: Physician;
  public intakeForm: IntakeForm;
  public product: string;
  public diagnosisOptions: string[] = [];
  public lcodeOptions: string[] = [];

  public lcodeText: string[] = [];
  public diagnosis: string[] = [];

  public intakeApproved = false;

  public intakeFormData: IntakeFormData;

  constructor(
    private readonly session: SessionService,
    private readonly patientApi: PatientService,
    private readonly physicianApi: PhysicianService,
    private readonly intakeFormApi: IntakeFormService,
    private readonly route: ActivatedRoute,
    private readonly router: Router) {
  }

  ngOnInit() {

    this.session.userAccount$.subscribe(u => this.isAdminView = (u.type === AccountType.Admin));

    this.intakeFormId = this.route.snapshot.paramMap.get('intakeFormId');

    if (this.route.snapshot.paramMap.has('vendorId')) {
      this.vendorId = this.route.snapshot.paramMap.get('vendorId');
    }

    this.intakeFormApi.get(this.intakeFormId).subscribe((intake: IntakeForm) => {

      // load intake
      this.intakeForm = intake;

      // load patient
      if (intake.patientId) {
        this.patientApi.get(intake.patientId).subscribe((patient: Patient) => this.patient = patient);
      }

      // load physician
      if (intake.physicianId) {
        this.physicianApi.get(intake.physicianId).subscribe((physician: Physician) => this.physician = physician);
      }

      // kind of ghetto infering all this but for now it works.
      const question = this.intakeForm.questions.filter(q => q.key === 'PainChart');
      const painArea = question[0].answers[0].text;

      // the product, lcode and diagnosis options are all distinct via the PainArea
      this.setDiagnosis(painArea);

    });

  }

  onIntakeApproval(data: IntakeFormData) {

    if (data) {
      this.intakeFormData = data;
    } else {
      // just set it to the default so the next page will display everything
      this.intakeFormData = new IntakeFormData();
      this.intakeFormData.diagnosisSelections = this.diagnosisOptions;
      this.intakeFormData.lcodeSelections = this.lcodeOptions;
    }
    this.intakeApproved = true;
  }

  onPrescriptionApproval() {
    if (this.isAdminView) {
      this.router.navigate(['vendor', this.vendorId, 'view']);
    } else {
      this.router.navigate([RouteUrls.PhysicianDashboardComponent]);
    }
  }


  private setDiagnosis(key: string) {

    switch (key.toUpperCase()) {
      case 'RIGHT ANKLE':
        this.product = 'Ankle Brace';
        this.diagnosisOptions.push('m19.071 primary osteoarthritis, right ankle and foot');
        this.diagnosisOptions.push('m25.579 pain in joint, ankle');
        this.diagnosisOptions.push('m25.571 pain in right ankle and joints of right foot');
        this.diagnosisOptions.push('m25.371 instability in right ankle');
        this.lcodeOptions.push('L1906 Ankle Foot Orthosis, Plastic or Other Material With Ankle Joint, Prefabricated, Includes Fitting and Adjustment');
        break;
      case 'LEFT ANKLE':
        this.product = 'Ankle Brace';
        this.diagnosisOptions.push('m19.072 primary osteoarthritis, left ankle and foot');
        this.diagnosisOptions.push('m25.579 pain in joint, ankle');
        this.diagnosisOptions.push('m25.572 pain in left ankle and joints of left foot');
        this.diagnosisOptions.push('m25.372 instability left ankle');
        this.lcodeOptions.push('L1906 Ankle Foot Orthosis, Plastic or Other Material With Ankle Joint, Prefabricated, Includes Fitting and Adjustment');
        break;
      case 'RIGHT KNEE ':
        this.product = 'Knee Brace';
        this.diagnosisOptions.push('m17.11 unilateral osteoarthritis of the right knee');
        this.diagnosisOptions.push('m23.51 chronic instability of the right knee');
        this.lcodeOptions.push('L1833 (Hinged Wraparound Knee Support)');
        this.lcodeOptions.push('L2397 (Universal Suspension Sleeve for ROM Hinged Knee Braces)');
        break;
      case 'LEFT KNEE':
        this.product = 'Knee Brace';
        this.diagnosisOptions.push('m17.12 unilateral osteoarthritis of left knee');
        this.diagnosisOptions.push('m23.52 chronic instability of the left knee');
        this.lcodeOptions.push('L1833 (Hinged Wraparound Knee Support)');
        this.lcodeOptions.push('L2397 (Universal Suspension Sleeve for ROM Hinged Knee Braces)');
        break;
      case 'RIGHT WRIST':
        this.product = 'Wrist Brace';
        this.diagnosisOptions.push('m19.031 primary osteoarthritis, right wrist');
        this.diagnosisOptions.push('g56.0 carpal tunnel syndrome');
        this.diagnosisOptions.push('m19.041 osteoarthritis right hand');
        this.lcodeOptions.push('L3908 wrist hand orthosis, includes one or more non-torsional joint(s), elastic bands, turnbuckles, may include a soft interface and straps. it is prefabricated and off the shelf.');
        break;
      case 'LEFT WRIST':
        this.product = 'Wrist Brace';
        this.diagnosisOptions.push('m19.032 primary osteoarthritis, left wrist');
        this.diagnosisOptions.push('g56.0 carpal tunnel syndrome');
        this.diagnosisOptions.push('m19.042 osteoarthritis left hand');
        this.lcodeOptions.push('L3908 wrist hand orthosis, includes one or more non-torsional joint(s), elastic bands, turnbuckles, may include a soft interface and straps. it is prefabricated and off the shelf.');
        break;
      case 'RIGHT ELBOW':
        this.product = 'Elbow Brace';
        this.diagnosisOptions.push('m24.221 disorder of ligament, right elbow');
        this.diagnosisOptions.push('m12.821 other specific arthropathy, not elsewhere specified, right elbow');
        this.diagnosisOptions.push('m12.821 pain in right elbow');
        this.diagnosisOptions.push('g89.4 chronic pain');
        this.lcodeOptions.push('L3761 Elbow Orthosis, With Adjustable Position Locking Joint(s), Prefabricated, Includes Fitting and Adjustments, Any Type.');
        break;
      case 'LEFT ELBOW':
        this.product = 'Elbow Brace';
        this.diagnosisOptions.push('m24.221 disorder of ligament, left  elbow');
        this.diagnosisOptions.push('m12.821 other specific arthropathy, not elsewhere specified, left elbow');
        this.diagnosisOptions.push('m25.521 pain in left elbow');
        this.diagnosisOptions.push('g89.4 chronic pain');
        this.lcodeOptions.push('L3761 Elbow Orthosis, With Adjustable Position Locking Joint(s), Prefabricated, Includes Fitting and Adjustments, Any Type.');
        break;
      case 'RIGHT SHOULDER':
        this.product = 'Shoulder Brace';
        this.diagnosisOptions.push('m25.511 pain in right shoulder');
        this.diagnosisOptions.push('m19.011 primary osteoarthritis, right shoulder');
        this.lcodeOptions.push('L3960 (Shoulder Elbow Wrist Hand Orthosis, Abduction Positioning, Airplane Design, Prefabricated, Includes Fitting and Adjustment.)');
        break;
      case 'LEFT SHOULDER':
        this.product = 'Shoulder Brace';
        this.diagnosisOptions.push('m25.512 pain in left shoulder ');
        this.diagnosisOptions.push('m19.012 primary osteoarthritis, left shoulder');
        this.lcodeOptions.push('L3960(Shoulder Elbow Wrist Hand Orthosis, Abduction Positioning, Airplane Design, Prefabricated, Includes Fitting and Adjustment.)');
        break;
      case 'LOWER BACK':
        this.product = 'Back Brace';
        this.diagnosisOptions.push('m54.5 low back pain');
        this.diagnosisOptions.push('m53.2x7 spinal instabilities, lumbosacral region');
        this.diagnosisOptions.push('g89.4 chronic pain');
        this.diagnosisOptions.push('m51.36 lumbar disc degeneration');
        this.lcodeOptions.push('L0650 (Lumbar-sacral orthosis. Sagittal control with rigid anterior and posterior panels, posterior panels, posterior extends from Sacrococcygeal junction to the T-9 vertebra, lateral strength, with rigid lateral panels, prefabricated and off the shelf. Custom fitting of the orthosis is not required and the patient or an assisting care giver can apply the prescribed orthotic device with minimal self-adjusting.)');
        break;

    }
  }

}
