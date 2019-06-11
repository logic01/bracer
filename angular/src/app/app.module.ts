import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AdminModule } from './admin/admin.module';
import { AgentModule } from './agent/agent.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DesignModule } from './design/design.module';
import { DocumentModule } from './document/document.module';
import { LoginModule } from './login/login.module';
import { PhysicianModule } from './physician/physician.module';
import { PipesModule } from './pipes/pipes.module';
import { MaskService } from './services/mask.service';
import { VendorModule } from './vendor/vendor.module';

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
    DocumentModule,
    PipesModule
  ],
  providers: [MaskService],
  bootstrap: [AppComponent]
})
export class AppModule { }
