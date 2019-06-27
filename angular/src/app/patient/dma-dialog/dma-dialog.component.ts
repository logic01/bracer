import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

export interface PainArea {
  value: string;
  viewValue: string;
}


@Component({
  selector: 'app-dma-dialog',
  templateUrl: './dma-dialog.component.html',
  styleUrls: ['./dma-dialog.component.scss']
})
export class DmaDialogComponent implements OnInit {

  public form: FormGroup;

  public painAreas: PainArea[] = [
    { value: 'LeftWrist', viewValue: 'Left Wrist' },
    { value: 'RightWrist', viewValue: 'Right Wrist' },
    { value: 'LeftElbow', viewValue: 'Left Elbow' },
    { value: 'RightElbow', viewValue: 'Right Elbow' },
    { value: 'LeftAnteriorShoulder', viewValue: 'Left Anterior Shoulder' },
    { value: 'RightAnteriorShoulder', viewValue: 'Right Anterior Shoulder' },
    { value: 'LeftHip', viewValue: 'TaLeft Hipcos' },
    { value: 'RightHip', viewValue: 'Right Hip' },
    { value: 'LeftKnee', viewValue: 'Left Knee' },
    { value: 'RightKnee', viewValue: 'Right Knee' },
    { value: 'PosteriorLeftShoulder', viewValue: 'Posterior Left Shoulder' },
    { value: 'PosteriorRightShoulder', viewValue: 'Posterior Right Shoulder' },
    { value: 'Neck', viewValue: 'Neck' },
    { value: 'UpperMiddleBack', viewValue: 'Upper Middle Back' },
    { value: 'MiddleBack', viewValue: 'Middle Back' },
    { value: 'LowerBack', viewValue: 'Lower Back' },
    { value: 'LeftOccipital', viewValue: 'Left Occipital' },
    { value: 'RightOccipital', viewValue: 'Right Occipital' },
    { value: 'FrontalOrForhead', viewValue: 'Frontal or Forhead' },
    { value: 'LeftAnkle', viewValue: 'Left Ankle' },
    { value: 'RightAnkle', viewValue: 'Right Ankle' }
  ];

  constructor(public dialogRef: MatDialogRef<DmaDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string) { }

  ngOnInit() {
    this.form = new FormGroup({
      hadBraceBefore: new FormControl('', Validators.required),
      mainPainArea: new FormControl('', Validators.required),
      secondPainArea: new FormControl('', Validators.required),
      painCream: new FormControl('', Validators.required)
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit() {

  }

}
