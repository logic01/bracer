import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AccountCreateModule } from './account/create/account-create.module';
import { AdminModule } from './admin/admin.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DesignModule } from './design/design.module';
import { LoginModule } from './login/login.module';
import { PhysicianModule } from './physician/physician.module';
import { VendorModule } from './vendor/vendor.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    DesignModule,
    LoginModule,
    AccountCreateModule,
    VendorModule,
    AdminModule,
    PhysicianModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
