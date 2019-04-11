import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-heartburn-acid-rx-only',
  templateUrl: './heartburn-acid-rx-only.component.html',
  styleUrls: ['./heartburn-acid-rx-only.component.scss']
})
export class HeartburnAcidRxOnlyComponent implements OnInit {

  form: FormGroup;

  constructor() { }

  ngOnInit() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q1_other: new FormControl(''),
      q2_1: new FormControl(''),
      q2_2: new FormControl(''),
      q2_3: new FormControl(''),
      q2_4: new FormControl(''),
      q2_other: new FormControl('')
    });
  }
}
