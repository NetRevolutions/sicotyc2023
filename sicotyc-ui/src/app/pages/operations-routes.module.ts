import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { OperacionesComponent } from './operaciones/operaciones.component';
import { CalculoTarifasComponent } from './operaciones/calculo-tarifas/calculo-tarifas.component';
import { CreacionServicioComponent } from './operaciones/creacion-servicio/creacion-servicio.component';
import { EvaluacionServicioComponent } from './operaciones/evaluacion-servicio/evaluacion-servicio.component';


const operationsRoutes: Routes = [
  { path: '', component: OperacionesComponent, data: {title: 'Operaciones' } },
  { path: 'calculo-tarifas', component: CalculoTarifasComponent, data: {title: 'Calculo de Tarifas' } },
  { path: 'evaluacion-servicio', component: EvaluacionServicioComponent, data: {title: 'Evaluacion de Servicio' } },
  { path: 'creacion-servicio', component: CreacionServicioComponent, data: {title: 'Creacion de Servicio' } }
];


@NgModule({
  imports: [RouterModule.forChild(operationsRoutes)],
  exports: [RouterModule]
})
export class OperationsRoutesModule { }
