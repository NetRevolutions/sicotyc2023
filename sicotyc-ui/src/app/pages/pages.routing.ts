import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { DashboardComponent } from './dashboard/dashboard.component';
import { Grafica1Component } from './dashboard/grafica1/grafica1.component';
import { PagesComponent } from './pages.component';
import { ProgressComponent } from './dashboard/progress/progress.component';
import { CalculoTarifasComponent } from './operaciones/calculo-tarifas/calculo-tarifas.component';
import { CreacionServicioComponent } from './operaciones/creacion-servicio/creacion-servicio.component';
import { EvaluacionServicioComponent } from './operaciones/evaluacion-servicio/evaluacion-servicio.component';
import { OperacionesComponent } from './operaciones/operaciones.component';
import { JuegosAzarComponent } from './dashboard/juegos-azar/juegos-azar.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { PromesasComponent } from './promesas/promesas.component';
import { RxjsComponent } from './rxjs/rxjs.component';

const routes: Routes = [
    { 
        // Rutas Autenticadas
        path:'dashboard', 
        component: PagesComponent,
        children: [
            { path: '', component: DashboardComponent, data: {title: 'Dashboard' } },
            { path: 'progress', component: ProgressComponent, data: {title: 'Progress Bar' } },
            { path: 'grafica1', component: Grafica1Component, data: {title: 'Grafica 1' } },
            { path: 'juegos-azar', component: JuegosAzarComponent, data: {title: 'Juegos Azar' } },
            { path: 'promesas', component: PromesasComponent, data: {title: 'Promesas' } },
            { path: 'rxjs', component: RxjsComponent, data: {title: 'rxJS' } },
            { path: 'account-settings', component: AccountSettingsComponent, data: {title: 'Account Settings' } },
        ] 
    },
    {
        path: 'operaciones',
        component: PagesComponent,
        children: [
            { path: '', component: OperacionesComponent, data: {title: 'Operaciones' } },
            { path: 'calculo-tarifas', component: CalculoTarifasComponent, data: {title: 'Calculo de Tarifas' } },
            { path: 'evaluacion-servicio', component: EvaluacionServicioComponent, data: {title: 'Evaluacion de Servicio' } },
            { path: 'creacion-servicio', component: CreacionServicioComponent, data: {title: 'Creacion de Servicio' } }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PagesRoutingModule {}