import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-general-dme-and-rx',
  templateUrl: './general-dme-and-rx.component.html',
  styleUrls: ['./general-dme-and-rx.component.scss']
})
export class GeneralDmeAndRxComponent implements OnInit {

  form: FormGroup;

  constructor() { }

  ngOnInit() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q2: new FormControl('', Validators.required),
      q3: new FormControl('', Validators.required),
      q4: new FormControl('', Validators.required),
      q5: new FormControl('', Validators.required),
      q6: new FormControl('', Validators.required),
      q7: new FormControl('', Validators.required),
      q8: new FormControl('', Validators.required),
      q9: new FormControl('', Validators.required),
      q10: new FormControl('', Validators.required),
      q11: new FormControl('', Validators.required),
      q12: new FormControl('', Validators.required),
      q13: new FormControl('', Validators.required),
      q14: new FormControl('', Validators.required),
      q15: new FormControl('', Validators.required),
      q16: new FormControl('', Validators.required),
      q17: new FormControl('', Validators.required),
      q18: new FormControl('', Validators.required)
    });

  }

}
