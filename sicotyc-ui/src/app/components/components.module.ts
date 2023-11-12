import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncrementadorComponent } from './incrementador/incrementador.component';
import { FormsModule } from '@angular/forms';
import { SimuladorTinkaComponent } from './simulador-tinka/simulador-tinka.component';



@NgModule({
  declarations: [
    IncrementadorComponent,
    SimuladorTinkaComponent
  ],
  exports: [
    IncrementadorComponent,
    SimuladorTinkaComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class ComponentsModule { }
