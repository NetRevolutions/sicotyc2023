import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncrementadorComponent } from './incrementador/incrementador.component';
import { FormsModule } from '@angular/forms';
import { SimuladorTinkaComponent } from './simulador-tinka/simulador-tinka.component';
import { DonaComponent } from './dona/dona.component';
import { NgChartsModule } from 'ng2-charts';
import { RatingComponent } from './rating/rating.component';
import { ModalImageComponent } from './modal-image/modal-image.component';
import { PaginationComponent } from './pagination/pagination.component';



@NgModule({
  declarations: [
    IncrementadorComponent,
    SimuladorTinkaComponent,
    DonaComponent,
    RatingComponent,
    ModalImageComponent,
    PaginationComponent
  ],
  exports: [
    IncrementadorComponent,
    SimuladorTinkaComponent,
    DonaComponent,
    RatingComponent,
    ModalImageComponent,
    PaginationComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgChartsModule
  ]
})
export class ComponentsModule { }
