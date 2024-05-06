import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

// Guards
import { AuthGuard, CanMatch } from '../guards/auth.guard';

// Components
import { PagesComponent } from './pages.component';

// Dashboard
// Operaciones
// Mantenimientos



const routes: Routes = [
    { 
        // Rutas Autenticadas
        path:'dashboard', 
        component: PagesComponent,
        canActivate: [ AuthGuard ],
        canLoad: [ CanMatch ],
        loadChildren: () => import('./dashboard-routes.module').then( m => m.DashboardRoutesModule ) 
    },
    {
        path: 'operaciones',
        component: PagesComponent,
        loadChildren: () => import('./operations-routes.module').then( m => m.OperationsRoutesModule )
    },
    {
        path: 'mantenimientos',
        component: PagesComponent,
        canActivate: [ AuthGuard ],
        canLoad: [ CanMatch ],
        loadChildren: () => import('./maintenance-routes.module').then( m => m.MaintenanceRoutesModule )
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PagesRoutingModule {}