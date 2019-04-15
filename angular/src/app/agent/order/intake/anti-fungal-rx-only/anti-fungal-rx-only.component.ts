import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-anti-fungal-rx-only',
  templateUrl: './anti-fungal-rx-only.component.html',
  styleUrls: ['./anti-fungal-rx-only.component.scss']
})
export class AntiFungalRxOnlyComponent implements OnInit {

  form: FormGroup;

  constructor() { }

  ngOnInit() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q2: new FormControl('', Validators.required),
      q3: new FormControl('', Validators.required)
    });
  }

  onSubmit(){

  }
}
