import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Components
import { DashboardComponent } from './dashboard/dashboard.component';
import { Grafica1Component } from './dashboard/grafica1/grafica1.component';
import { ProgressComponent } from './dashboard/progress/progress.component';
import { JuegosAzarComponent } from './dashboard/juegos-azar/juegos-azar.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { PromesasComponent } from './promesas/promesas.component';
import { RxjsComponent } from './rxjs/rxjs.component';
import { ProfileComponent } from './profile/profile.component';
import { SearchComponent } from './search/search.component';

const dashboardRoutes: Routes = [
  { path: '', component: DashboardComponent, data: {title: 'Dashboard' } },
  { path: 'progress', component: ProgressComponent, data: {title: 'Progress Bar' } },
  { path: 'grafica1', component: Grafica1Component, data: {title: 'Grafica 1' } },
  { path: 'juegos-azar', component: JuegosAzarComponent, data: {title: 'Juegos Azar' } },
  { path: 'promesas', component: PromesasComponent, data: {title: 'Promesas' } },
  { path: 'rxjs', component: RxjsComponent, data: {title: 'rxJS' } },
  { path: 'account-settings', component: AccountSettingsComponent, data: {title: 'Account Settings' } },
  { path: 'search/:term', component: SearchComponent, data: {title: 'Busquedas'}},
  { path: 'profile', component: ProfileComponent, data: {title: 'Perfil de Usuario' } },
];

@NgModule({
  imports: [RouterModule.forChild(dashboardRoutes)],
  exports: [RouterModule]
})
export class DashboardRoutesModule { }
