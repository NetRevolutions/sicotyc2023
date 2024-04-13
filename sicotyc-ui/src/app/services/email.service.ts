import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { environment } from 'src/environments/environment.development';

import { ValidationErrorsCustomizeService } from './validation-errors-customize.service';

// Interfaces
import { IEmailMetadata } from '../interfaces/email-metadata.interface';

// Models
import { User } from '../models/user.model';



const base_url = environment.base_url;

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  public user?: User;
  
  constructor(
    private http: HttpClient,
              private router: Router,
              private validationErrorsCustomize: ValidationErrorsCustomizeService
  ) { }

  get token(): string {

    return localStorage.getItem('token') || '';
  };

  get uid(): string {
    return this.user?.id || '';
  };

  get roles(): string[] {
    return this.user?.roles || [];
  };

  get headers() {
    return {
      headers: {
        'x-token': this.token
      }
    }
  };

  sendResetPasswordEmail(emailMetadata: IEmailMetadata) {
    //console.log(emailMetadata);
    const data = { 
      ToAddress : emailMetadata.toAddress,
      Subject: emailMetadata.subject,
      Body: emailMetadata.body,
      IsHtml: emailMetadata.isHTML
    };
    const url = `${ base_url }/email/sendMail`;
    return this.http.post(url, data)
      .pipe(
        catchError(error => {
          return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
        })
      );
  };

}
