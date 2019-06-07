import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { Patient } from '../models/patient.model';
import { Physician } from '../models/physician.model';
import { PatientService } from '../services/api/patient.service';
import { PhysicianService } from '../services/api/physician.service';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements OnInit {

  @Input() patient$: Observable<Patient> = this.patientApi.get('5');
  @Input() physician$: Observable<Physician> = this.physicianApi.get('3');

  public dateOfService = Date.now();
  public form: FormGroup;
  public ipAddress = '207.243.55.226';

  constructor(
    private readonly patientApi: PatientService,
    private readonly physicianApi: PhysicianService) {


    this.form = new FormGroup({
      ICD10: new FormControl('', [Validators.required])
    });

  }

  ngOnInit() {
  }

}
