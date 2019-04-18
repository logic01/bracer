import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-footbath-rx-only',
  templateUrl: './footbath-rx-only.component.html',
  styleUrls: ['./footbath-rx-only.component.scss']
})
export class FootbathRxOnlyComponent implements OnInit {

  form: FormGroup;

  constructor() { }

  ngOnInit() {
    this.form = new FormGroup({
      q1: new FormControl('', Validators.required),
      q2: new FormControl('', Validators.required),
      q3: new FormControl('', Validators.required),
      q4: new FormControl('', Validators.required)
    });
  }

  onSubmit(){
    
  }

}
