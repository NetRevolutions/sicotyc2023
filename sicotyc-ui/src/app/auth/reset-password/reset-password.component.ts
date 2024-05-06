import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';

// Interfaces
import { IEmailMetadata } from 'src/app/interfaces/email-metadata.interface';

// Services
import { UserService } from 'src/app/services/user.service';
import { EmailService } from 'src/app/services/email.service';
import { IEmailItem } from 'src/app/interfaces/email-item.interface';
import { IResetPassword } from 'src/app/interfaces/reset-password.interface';
import { IResultProcess } from 'src/app/interfaces/result-process.interface';
import { environment } from 'src/environments/environment.development';

const ui_url = environment.ui_url;

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: [ '../login/login.component.css']
})
export class ResetPasswordComponent implements OnInit{
  public hasToken: boolean = false;
  public resetPasswordForm: FormGroup;
  public tokenReset?: string = '';
  public firstName?: string = '';
  public lastName?: string = '';
  public email?: string = '';
  public id?: string = '';

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private userService: UserService,
    private emailService: EmailService
  ) {
    this.resetPasswordForm = this.fb.group({});
  }

  ngOnInit(): void {
    const urlParams = new URLSearchParams(window.location.search);    
    if (urlParams.get('token-reset-pwd') != null)
    {
      this.hasToken = true;
      this.tokenReset = decodeURIComponent(urlParams.get('token-reset-pwd')?.toString() ?? '');
      this.firstName = urlParams.get('firstName')?.toString();
      this.lastName = urlParams.get('lastName')?.toString();
      this.email = urlParams.get('email')?.toString();
      this.id = urlParams.get('id')?.toString();
    }
    this.loadForm();
  };

  loadForm() {
    if (this.hasToken) {
      this.resetPasswordForm = this.fb.group({
        firstName: [{value: this.firstName, disabled: true}], // Campo de Solo lectura
        lastName: [{value: this.lastName, disabled: true}], // Campo de Solo lectura
        email: [{value: this.email, disabled: true}], // Campo de Solo lectura    
        newPassword: ['', [Validators.required, Validators.minLength(10)]]
      });
    }
    else {
      this.resetPasswordForm = this.fb.group({
         email: ['', [Validators.required, Validators.email]] 
      });
    }
  };

  requestReset() {
    this.userService.getUserByEmail(this.resetPasswordForm.get('email')?.value)
    .subscribe({
      next: (user) => {
        const { id, firstName, lastName } = user.data;
        this.userService.getResetToken(id)
        .subscribe({
          next: (token: any) => {
            let tokenFormatted = encodeURIComponent(token.token); 
            let toAddressItem: IEmailItem = {
              email: this.resetPasswordForm.get('email')?.value, name: firstName + ' ' + lastName
            };
            let emailMetadata: IEmailMetadata = {
              toAddress: [toAddressItem],
              subject: 'SICOTYC - Correo de reseteo de contraseña',
              body: `Estimado ${ firstName } ${ lastName } para poder resetear su contraseña agradeceremos abrir en un navegador el siguiente enlace: <a href='${ui_url}/reset-password?token-reset-pwd=${ tokenFormatted }&id=${id}&firstName=${firstName}&lastName=${lastName}&email=${this.resetPasswordForm.get('email')?.value}' target='_blank'>${ui_url}/reset-password?token-reset-pwd=${ tokenFormatted }&id=${id}&firstName=${firstName}&lastName=${lastName}&email=${this.resetPasswordForm.get('email')?.value}</a>`,
              isHTML: true
            };

            this.emailService.sendResetPasswordEmail(emailMetadata)
            .subscribe({
              next: ((resp: IResultProcess) => {
                if (resp.success == true) {
                  let message = 'se acaba de enviar un correo con las instrucciones para resetear la contraseña, verifique!!!'
                  Swal.fire('Exito', message, 'success');
                }
              }),
              error: (err) => {
                console.log(err);
                Swal.fire('Error', err.message, 'error');    
              },
              complete: () => console.info('envio exitoso del correo para resetear contraseña')
            });
          },
          error: (err) => {
            console.log(err);
            Swal.fire('Error', err.message, 'error');
          },
          complete: () => console.info('obtencion exitosa del token para reseteo de contraseña')
        })        
      },
      error: (err) => {
        console.log(err);
        Swal.fire('Error', err.message, 'error');
      },
      complete: () => console.info('obtencion de datos de usuario realizado')
    })
  };

  resetPassword() {
    let formData: IResetPassword = {
      userId: this.id,
      token: this.tokenReset,
      newPassword: this.resetPasswordForm.get('newPassword')?.value

    };
    this.userService.resetPassword(formData)
    .subscribe({
      next: ((resp: IResultProcess) => {
        if (resp.success == true) {
          Swal.fire('Exito', resp.message, 'success')
          .then((result) => {
            if (result.isConfirmed) {
              this.router.navigateByUrl('/login');
            }
          })
        }
      }),
      error: (err) => {
        //console.log(err);
        Swal.fire('Error', err.message, 'error');
      },
      complete: () => console.info('Reseteo del password exitoso')
    });
  };

}
