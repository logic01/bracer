import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  step = 3;

  constructor() { }

  ngOnInit() {
  }

  next() {
    if (this.step <= 3) {
      this.step++;
    } else {
      this.submit();
    }
  }

  submit() {
    console.log('submit');
  }
}
