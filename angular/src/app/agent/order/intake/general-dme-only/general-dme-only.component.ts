import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-general-dme-only',
  templateUrl: './general-dme-only.component.html',
  styleUrls: ['./general-dme-only.component.scss']
})
export class GeneralDmeOnlyComponent implements OnInit {

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
      q10: new FormControl('', Validators.required)
    });

  }
}
