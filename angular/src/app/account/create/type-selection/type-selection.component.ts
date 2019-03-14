import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RouteUrls } from '../../../constants/routes';

@Component({
  selector: 'app-type-selection',
  templateUrl: './type-selection.component.html',
  styleUrls: ['./type-selection.component.scss']
})
export class TypeSelectionComponent implements OnInit {

  constructor(private readonly router: Router) { }

  ngOnInit() {
  }

  physician_button_click() {
    this.router.navigateByUrl(RouteUrls.CreatePhysicianComponent);
  }

  vendor_button_click() {
    this.router.navigateByUrl(RouteUrls.CreateVendorComponent);
  }

  admin_button_click() {
    this.router.navigateByUrl(RouteUrls.CreateAdminComponent);
  }

}
