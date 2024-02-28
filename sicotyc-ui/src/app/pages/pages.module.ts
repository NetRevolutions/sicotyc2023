import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgChartsModule } from 'ng2-charts';

// Modulos
import { SharedModule } from '../shared/shared.module';
import { ComponentsModule } from '../components/components.module';

// Componentes
import { DashboardComponent } from './dashboard/dashboard.component';
import { Grafica1Component } from './dashboard/grafica1/grafica1.component';
import { PagesComponent } from './pages.component';
import { ProgressComponent } from './dashboard/progress/progress.component';
import { JuegosAzarComponent } from './dashboard/juegos-azar/juegos-azar.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { PromesasComponent } from './promesas/promesas.component';
import { RxjsComponent } from './rxjs/rxjs.component';
import { ProfileComponent } from './profile/profile.component';
import { UsersComponent } from './maintenance/users/users.component';
import { MaintenanceComponent } from './maintenance/maintenance.component';

@NgModule({
  declarations: [
    DashboardComponent,
    ProgressComponent,
    Grafica1Component,
    PagesComponent,
    JuegosAzarComponent,
    AccountSettingsComponent,
    PromesasComponent,
    RxjsComponent,
    ProfileComponent,
    UsersComponent,
    MaintenanceComponent,
    
  ],
  exports: [
    DashboardComponent,
    ProgressComponent,
    Grafica1Component,
    PagesComponent,
    AccountSettingsComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,    
    SharedModule,
    ComponentsModule,
    NgChartsModule
  ],
})
export class PagesModule { }
