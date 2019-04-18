import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-pain-rx-only',
  templateUrl: './pain-rx-only.component.html',
  styleUrls: ['./pain-rx-only.component.scss']
})
export class PainRxOnlyComponent implements OnInit {

  form: FormGroup;

  constructor() { }

  ngOnInit() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q2: new FormControl('', Validators.required),
      q3: new FormControl('', Validators.required),
      q4: new FormControl('', Validators.required),
      q8: new FormControl('', Validators.required),
      q9: new FormControl('', Validators.required),
      q10: new FormControl('', Validators.required),
      q11: new FormControl('', Validators.required),
      q12: new FormControl('', Validators.required),
      q5_1: new FormControl(''),
      q5_2: new FormControl(''),
      q5_3: new FormControl(''),
      q5_4: new FormControl(''),
      q5_5: new FormControl(''),
      q5_6: new FormControl(''),
      q5_7: new FormControl(''),
      q6_1: new FormControl(''),
      q6_2: new FormControl(''),
      q6_3: new FormControl(''),
      q6_4: new FormControl(''),
      q6_5: new FormControl(''),
      q6_6: new FormControl(''),
      q6_7: new FormControl(''),
      q7_1: new FormControl(''),
      q7_2: new FormControl(''),
      q7_3: new FormControl(''),
      q7_4: new FormControl(''),
      q7_5: new FormControl(''),
      q7_6: new FormControl('')
    });
  }

  onSubmit(){
    
  }
}
