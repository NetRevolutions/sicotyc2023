import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

// Guards
import { AuthGuard } from '../guards/auth.guard';

// Dashboard
import { DashboardComponent } from './dashboard/dashboard.component';
import { Grafica1Component } from './dashboard/grafica1/grafica1.component';
import { PagesComponent } from './pages.component';
import { ProgressComponent } from './dashboard/progress/progress.component';
import { JuegosAzarComponent } from './dashboard/juegos-azar/juegos-azar.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { PromesasComponent } from './promesas/promesas.component';
import { RxjsComponent } from './rxjs/rxjs.component';
import { ProfileComponent } from './profile/profile.component';

// Operaciones
import { OperacionesComponent } from './operaciones/operaciones.component';
import { CalculoTarifasComponent } from './operaciones/calculo-tarifas/calculo-tarifas.component';
import { CreacionServicioComponent } from './operaciones/creacion-servicio/creacion-servicio.component';
import { EvaluacionServicioComponent } from './operaciones/evaluacion-servicio/evaluacion-servicio.component';

// Mantenimientos
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { UsersComponent } from './maintenance/users/users.component';
import { UserComponent } from './maintenance/users/user.component';
import { LookupCodeGroupsComponent } from './maintenance/lookup/lookup-code-groups.component';
import { LookupCodeGroupComponent } from './maintenance/lookup/lookup-code-group.component';
import { LookupCodesComponent } from './maintenance/lookup/lookup-codes.component';
import { LookupCodeComponent } from './maintenance/lookup/lookup-code.component';


const routes: Routes = [
    { 
        // Rutas Autenticadas
        path:'dashboard', 
        component: PagesComponent,
        canActivate: [ AuthGuard ],
        children: [
            { path: '', component: DashboardComponent, data: {title: 'Dashboard' } },
            { path: 'progress', component: ProgressComponent, data: {title: 'Progress Bar' } },
            { path: 'grafica1', component: Grafica1Component, data: {title: 'Grafica 1' } },
            { path: 'juegos-azar', component: JuegosAzarComponent, data: {title: 'Juegos Azar' } },
            { path: 'promesas', component: PromesasComponent, data: {title: 'Promesas' } },
            { path: 'rxjs', component: RxjsComponent, data: {title: 'rxJS' } },
            { path: 'account-settings', component: AccountSettingsComponent, data: {title: 'Account Settings' } },
            { path: 'profile', component: ProfileComponent, data: {title: 'Perfil de Usuario' } },
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
    },
    {
        path: 'mantenimientos',
        component: PagesComponent,
        canActivate: [ AuthGuard ],
        children: [
            { path: '', component: MaintenanceComponent, data: {title: 'Mantenimientos' } },
            { path: 'users', component: UsersComponent, data: {title: 'Usuarios de Aplicacion'}},
            { path: 'users/:id', component: UserComponent, data: {title: 'Usuario de Aplicacion'}},
            { path: 'lookupCodeGroups', component: LookupCodeGroupsComponent, data: {title: 'Lookup Code Groups'}},
            { path: 'lookupCodeGroups/:id', component: LookupCodeGroupComponent, data: {title: 'Lookup Code Group'}},
            { path: 'lookupCodeGroups/:id/lookupCodes', component: LookupCodesComponent, data: {title: 'Lookup Codes'}},
            { path: 'lookupCodeGroups/:id/lookupCodes/:lcId', component: LookupCodeComponent, data: {title: 'Lookup Code'}},
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PagesRoutingModule {}