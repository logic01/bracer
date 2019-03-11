import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { SignaturePad } from 'angular2-signaturepad/signature-pad';

@Component({
  selector: 'app-create-physician',
  templateUrl: './create-physician.component.html',
  styleUrls: ['./create-physician.component.scss']
})
export class CreatePhysicianComponent implements OnInit {

  @ViewChild(SignaturePad) signaturePad: SignaturePad;

  public accountForm: FormGroup;

  public signaturePadOptions: Object = {
    'canvasWidth': 1000,
    'canvasHeight': 100,
    'backgroundColor': 'rgb(211,211,211)'
  };

  constructor() { }

  ngOnInit() {
    this.accountForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      password_confirm: new FormControl('', Validators.required),
      first_name: new FormControl('', Validators.required),
      last_name: new FormControl('', Validators.required),
    });
  }

  drawComplete() {
    // will be notified of szimek/signature_pad's onEnd event
    console.log(this.signaturePad.toDataURL());
  }

  drawStart() {
    // will be notified of szimek/signature_pad's onBegin event
    console.log('begin drawing');
  }
}
