import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCheckboxChange, MatDialog } from '@angular/material';
import { SignatureDialogComponent } from 'src/app/document/signature-dialog/signature-dialog.component';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Patient } from 'src/app/models/patient.model';
import { Physician } from 'src/app/models/physician.model';

export class IntakeFormData {
  signature: string;
  diagnosisSelections: string[];
  lcodeSelections: string[];
  duration: string;
  notes: string;
}

@Component({
  selector: 'app-intake',
  templateUrl: './intake.component.html',
  styleUrls: ['./intake.component.scss']
})
export class IntakeComponent implements OnInit {

  @Input() isAdminView = true;
  @Input() patient: Patient;
  @Input() physician: Physician;
  @Input() intakeForm: IntakeForm;
  @Input() product: string;
  @Input() diagnosisOptions: string[] = [];
  @Input() lcodeOptions: string[] = [];

  @Output() formSubmitEvent = new EventEmitter<IntakeFormData | null>();

  public form: FormGroup;
  public today = Date.now();
  public signatureData: string;
  public diagnosisSelections: string[] = [];
  public lcodeSelections: string[] = [];

  constructor(private readonly dialog: MatDialog) {

    this.form = new FormGroup({
      diagnosis_other: new FormControl(''),
      lcode_other: new FormControl(''),
      additional_notes: new FormControl('', Validators.maxLength(500)),
      productDuration: new FormControl('', Validators.required),
      signature: new FormControl(['', Validators.required])
    });

  }

  ngOnInit() {
  }

  calcAge(): number {
    const timeDiff = Math.abs(Date.now() - (new Date(this.patient.dateOfBirth).getTime()));
    const age = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25);
    return age;
  }

  getAnswer(key: string) {

    let text = '';
    const questions = this.intakeForm.questions.filter(q => q.key === key);

    if (questions.length > 0) {
      for (const answer of questions[0].answers) {
        text += answer.text;
      }
    }

    return text;

  }

  onDiagnosisCheck(event: MatCheckboxChange, index: number) {
    if (event.checked) {
      this.diagnosisSelections.push(this.diagnosisOptions[index]);
    } else {
      this.diagnosisSelections = this.diagnosisSelections.filter(obj => obj !== this.diagnosisOptions[index]);
    }
  }

  onLCodeCheck(event: MatCheckboxChange, index: number) {

    if (event.checked) {
      this.lcodeSelections.push(this.lcodeOptions[index]);
    } else {
      this.lcodeSelections = this.lcodeSelections.filter(obj => obj !== this.lcodeOptions[index]);
    }
  }

  sign() {
    this.dialog
      .open(SignatureDialogComponent)
      .afterClosed()
      .subscribe(result => this.signatureData = result);
  }

  approve() {

    if (!this.form.valid || this.lcodeSelections.length === 0 || this.diagnosisSelections.length === 0 || !this.signatureData) {
      return;
    }

    const diagnosis_other = this.form.controls['diagnosis_other'].value;
    if (diagnosis_other) {
      this.diagnosisSelections.push(diagnosis_other);
    }

    const lcode_other = this.form.controls['lcode_other'].value;
    if (lcode_other) {
      this.diagnosisSelections.push(lcode_other);
    }

    const data = new IntakeFormData();

    data.signature = this.signatureData;
    data.diagnosisSelections = this.diagnosisSelections;
    data.lcodeSelections = this.lcodeSelections;
    data.notes = this.form.controls['additional_notes'].value;
    data.duration = this.form.controls['productDuration'].value;

    this.formSubmitEvent.emit(data);
  }

  next() {
    this.formSubmitEvent.emit();
  }

}
