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

const routes: Routes = [
    { 
        // Rutas Autenticadas
        path:'dashboard', 
        component: PagesComponent,
        children: [
            { path: '', component: DashboardComponent },
            { path: 'progress', component: ProgressComponent },
            { path: 'grafica1', component: Grafica1Component },
            { path: 'juegos-azar', component: JuegosAzarComponent },
            { path: 'account-settings', component: AccountSettingsComponent },
        ] 
    },
    {
        path: 'operaciones',
        component: PagesComponent,
        children: [
            { path: '', component: OperacionesComponent },
            { path: 'calculo-tarifas', component: CalculoTarifasComponent },
            { path: 'evaluacion-servicio', component: EvaluacionServicioComponent },
            { path: 'creacion-servicio', component: CreacionServicioComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PagesRoutingModule {}