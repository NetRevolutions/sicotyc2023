import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

// Components
import { Error400Component } from './error-400/error-400.component';
import { Error403Component } from './error-403/error-403.component';
import { Error404Component } from './error-404/error-404.component';
import { Error500Component } from './error-500/error-500.component';
import { Error503Component } from './error-503/error-503.component';

const routes: Routes = [
    { path: 'error-400', component: Error400Component },
    { path: 'error-403', component: Error403Component },
    { path: 'error-404', component: Error404Component },
    { path: 'error-500', component: Error500Component },
    { path: 'error-503', component: Error503Component },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ErrorRoutingModule {}