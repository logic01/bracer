import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-scar-rx-only',
  templateUrl: './scar-rx-only.component.html',
  styleUrls: ['./scar-rx-only.component.scss']
})
export class ScarRxOnlyComponent implements OnInit {

  form: FormGroup;

  constructor() { }

  ngOnInit() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q2: new FormControl('', Validators.required),
      q3: new FormControl(''),
      q3_other: new FormControl(''),
      q4_1: new FormControl(''),
      q4_2: new FormControl(''),
      q4_3: new FormControl(''),
      q4_4: new FormControl(''),
      q4_other: new FormControl('')
    });
  }

  onSubmit() {

  }

}
