import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import { CoreModule } from './core/core.module';
import { ShopModule } from './shop/shop.module';
import { AppRoutingModule } from './app-routing.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    AppRoutingModule
  ],
  providers: [
    {provide:HTTP_INTERCEPTORS, useClass:ErrorInterceptor,multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
