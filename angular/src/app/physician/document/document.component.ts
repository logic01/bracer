import { Component, OnInit, ViewChild } from '@angular/core';

import { SignaturePad } from 'angular2-signaturepad/signature-pad';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements OnInit {

  @ViewChild(SignaturePad) signaturePad: SignaturePad;

  public signaturePadOptions: Object = {
    'canvasWidth': 1000,
    'canvasHeight': 100,
    'backgroundColor': 'rgb(211,211,211)'
  };

  constructor() { }

  ngOnInit() {
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
