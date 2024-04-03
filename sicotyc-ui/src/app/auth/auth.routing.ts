import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RecoverPasswordComponent } from './recover-password/recover-password.component';
import { LockScreenComponent } from './lock-screen/lock-screen.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

const routes: Routes = [
    // Rutas no autenticadas
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'recover-password', component: RecoverPasswordComponent },
    { path: 'lock-screen', component: LockScreenComponent },
    { path: 'change-password', component: ChangePasswordComponent} 
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRoutingModule {}