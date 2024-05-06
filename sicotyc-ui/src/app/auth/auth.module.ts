import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http'

// Components
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LockScreenComponent } from './lock-screen/lock-screen.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { TermsConditionsPopupComponent } from './terms-conditions-popup/terms-conditions-popup.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';



@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    LockScreenComponent,
    ResetPasswordComponent,
    ChangePasswordComponent,
    TermsConditionsPopupComponent,
    ConfirmEmailComponent,
  ],
  exports: [
    LoginComponent,
    RegisterComponent,
    LockScreenComponent,
    ResetPasswordComponent,
    TermsConditionsPopupComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ]
})
export class AuthModule { }
