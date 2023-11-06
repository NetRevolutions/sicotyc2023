import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Error400Component } from './error-400/error-400.component';
import { Error403Component } from './error-403/error-403.component';
import { Error500Component } from './error-500/error-500.component';
import { Error503Component } from './error-503/error-503.component';
import { Error404Component } from './error-404/error-404.component';



@NgModule({
  declarations: [
    Error400Component,
    Error403Component,
    Error404Component,
    Error500Component,
    Error503Component
  ],
  exports: [
    Error400Component,
    Error403Component,
    Error404Component,
    Error500Component,
    Error503Component
  ],
  imports: [
    CommonModule
  ]
})
export class ErrorModule { }
