import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Modules
import { PagesRoutingModule } from './pages/pages.routing';
import { ErrorRoutingModule } from './error/error.routing';
import { AuthRoutingModule } from './auth/auth.routing';

// Componentes


const routes: Routes = [
  { path: '**', redirectTo: 'error-404', pathMatch:'full' },
];

@NgModule({
  imports: [
    RouterModule.forRoot( routes ),
    PagesRoutingModule,
    AuthRoutingModule,
    ErrorRoutingModule
  ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
