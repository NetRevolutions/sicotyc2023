import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Guards
import { AdminGuard } from '../guards/admin.guard';

// Components
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { UsersComponent } from './maintenance/users/users.component';
import { UserComponent } from './maintenance/users/user.component';
import { LookupCodeGroupsComponent } from './maintenance/lookup/lookup-code-groups.component';
import { LookupCodesComponent } from './maintenance/lookup/lookup-codes.component';
import { LookupCodeComponent } from './maintenance/lookup/lookup-code.component';
import { MenuComponent } from './maintenance/menu/menu.component';

const maintenanceRoutes: Routes = [
  { path: '', component: MaintenanceComponent, data: {title: 'Mantenimientos' } },
  { path: 'users', canActivate: [AdminGuard], component: UsersComponent, data: {title: 'Usuarios de Aplicacion'}},
  { path: 'users/:id', component: UserComponent, data: {title: 'Usuario de Aplicacion'}},
  { path: 'lookupCodeGroups', component: LookupCodeGroupsComponent, data: {title: 'Lookup Code Groups'}},
  { path: 'lookupCodeGroups/lookupCodes', component: LookupCodesComponent, data: {title: 'Lookup Codes'}},
  { path: 'lookupCodeGroups/:lcgId/lookupCodes/:lcId', component: LookupCodeComponent, data: {title: 'Lookup Code'}},
  { path: 'menu', component: MenuComponent, data: {title: 'Menu de la Aplicacion'}},
]


@NgModule({
  imports: [RouterModule.forChild(maintenanceRoutes)],
  exports: [RouterModule]
})
export class MaintenanceRoutesModule { }
