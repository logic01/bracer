import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCheckboxChange, MatDialog } from '@angular/material';
import { SignatureDialogComponent } from 'src/app/document/signature-dialog/signature-dialog.component';
import { IntakeForm } from 'src/app/models/intake-form.model';
import { Patient } from 'src/app/models/patient.model';
import { Physician } from 'src/app/models/physician.model';
import { Signature } from 'src/app/models/signature.model';
import { SignatureService } from 'src/app/services/api/signature.service';

export interface ICD10 {
  text: string;
}

export interface HCPCS {
  product: string;
  text: string;
}

@Component({
  selector: 'app-intake',
  templateUrl: './intake.component.html',
  styleUrls: ['./intake.component.scss']
})
export class IntakeComponent implements OnInit {

  @Input() patient: Patient;
  @Input() physician: Physician;
  @Input() intakeForm: IntakeForm;
  @Input() product: string;
  @Input() diagnosisOptions: ICD10[] = [];
  @Input() lcodeOptions: HCPCS[] = [];

  public form: FormGroup;
  public today = Date.now();
  public signatureData: string;
  public diagnosisSelections: ICD10[] = [];
  public lcodeSelections: HCPCS[] = [];

  constructor(
    private readonly signatureApi: SignatureService,
    private readonly dialog: MatDialog) {

    this.form = new FormGroup({
      diagnosis_other: new FormControl(''),
      lcode_other: new FormControl(''),
      additional_notes: new FormControl(''),
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
    const dialogRef = this.dialog.open(SignatureDialogComponent);

    dialogRef.afterClosed().subscribe(result => {

      const signature = new Signature();
      signature.content = result;
      this.signatureData = result;
      //      this.signatureApi.sign(id, signature).subscribe();

    });
  }

  onSubmit() {

    if (!this.form.valid || this.lcodeSelections.length === 0 || this.diagnosisSelections.length === 0 || !this.signatureData) {
      return;
    }

    //    this.formSubmitEvent.emit(admin);
  }


}
