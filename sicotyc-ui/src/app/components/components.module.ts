import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncrementadorComponent } from './incrementador/incrementador.component';
import { FormsModule } from '@angular/forms';
import { SimuladorTinkaComponent } from './simulador-tinka/simulador-tinka.component';
import { DonaComponent } from './dona/dona.component';
import { NgChartsModule } from 'ng2-charts';



@NgModule({
  declarations: [
    IncrementadorComponent,
    SimuladorTinkaComponent,
    DonaComponent
  ],
  exports: [
    IncrementadorComponent,
    SimuladorTinkaComponent,
    DonaComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgChartsModule
  ]
})
export class ComponentsModule { }
