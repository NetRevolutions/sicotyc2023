import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { Error404Component } from './pages/error/error-404/error-404.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { BreadcrumbsComponent } from './share/breadcrumbs/breadcrumbs.component';
import { SidebarComponent } from './share/sidebar/sidebar.component';
import { HeaderComponent } from './share/header/header.component';
import { ProgressComponent } from './pages/progress/progress.component';
import { Grafica1Component } from './pages/grafica1/grafica1.component';
import { PagesComponent } from './pages/pages.component';
import { Error500Component } from './pages/error/error-500/error-500.component';
import { Error400Component } from './pages/error/error-400/error-400.component';
import { Error403Component } from './pages/error/error-403/error-403.component';
import { Error503Component } from './pages/error/error-503/error-503.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    Error404Component,
    DashboardComponent,
    BreadcrumbsComponent,
    SidebarComponent,
    HeaderComponent,
    ProgressComponent,
    Grafica1Component,
    PagesComponent,
    Error500Component,
    Error400Component,
    Error403Component,
    Error503Component
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
