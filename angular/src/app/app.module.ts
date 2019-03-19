import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AdminModule } from './admin/admin.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DesignModule } from './design/design.module';
import { LoginModule } from './login/login.module';
import { PhysicianModule } from './physician/physician.module';
import { VendorModule } from './vendor/vendor.module';
import { SharedModule } from './shared/shared.module';
import { AgentModule } from './agent/agent.module';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    DesignModule,
    LoginModule,
    VendorModule,
    AdminModule,
    AgentModule,
    PhysicianModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
