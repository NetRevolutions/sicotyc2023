import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Componentes
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ProgressComponent } from './pages/progress/progress.component';
import { Grafica1Component } from './pages/grafica1/grafica1.component';
import { PagesComponent } from './pages/pages.component';
import { Error400Component } from './pages/error/error-400/error-400.component';
import { Error404Component } from './pages/error/error-404/error-404.component';
import { Error403Component } from './pages/error/error-403/error-403.component';
import { Error500Component } from './pages/error/error-500/error-500.component';
import { Error503Component } from './pages/error/error-503/error-503.component';

const routes: Routes = [
  { 
    // Rutas Autenticadas
    path:'', 
    component: PagesComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'progress', component: ProgressComponent },
      { path: 'grafica1', component: Grafica1Component },
      { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    ] 
  },

  // Rutas No Autenticadas
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  
  // Error Pages
  { path: 'error-400', component: Error400Component },
  { path: 'error-403', component: Error403Component },
  { path: 'error-404', component: Error404Component },
  { path: 'error-500', component: Error500Component },
  { path: 'error-503', component: Error503Component },

  { path: '**', redirectTo: 'error-404', pathMatch:'full' },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot( routes )
  ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
