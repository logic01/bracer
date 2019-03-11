import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AccountCreateModule } from './account/create/account-create.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DesignModule } from './design/design.module';
import { InventoryComponent } from './inventory/inventory.component';
import { LoginModule } from './login/login.module';
import { WelcomeModule } from './welcome/welcome.module';


@NgModule({
  declarations: [
    AppComponent,
    InventoryComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    DesignModule,
    LoginModule,
    WelcomeModule,
    AccountCreateModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
