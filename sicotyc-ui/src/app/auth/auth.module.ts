import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

// Components
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LockScreenComponent } from './lock-screen/lock-screen.component';
import { RecoverPasswordComponent } from './recover-password/recover-password.component';



@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    LockScreenComponent,
    RecoverPasswordComponent,
  ],
  exports: [
    LoginComponent,
    RegisterComponent,
    LockScreenComponent,
    RecoverPasswordComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule
  ]
})
export class AuthModule { }
