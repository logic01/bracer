import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RouteUrls } from '../../constants/routes';

@Component({
  selector: 'app-vendor-dashboard',
  templateUrl: './vendor-dashboard.component.html',
  styleUrls: ['./vendor-dashboard.component.scss']
})
export class VendorDashboardComponent implements OnInit {

  constructor(private readonly router: Router) { }

  ngOnInit() {
  }

  new_call_button_click() {
    this.router.navigateByUrl(RouteUrls.VendorCallComponent);
  }

}
