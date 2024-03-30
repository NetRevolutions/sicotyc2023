import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment.development';

import { ValidationErrorsCustomizeService } from './validation-errors-customize.service';

const base_url = environment.base_url;

@Injectable({
  providedIn: 'root'
})
export class SearchesService {

  constructor(
    private http: HttpClient,
    private router: Router,
    private validationErrorsCustomize: ValidationErrorsCustomizeService
    ) { }

  get token(): string {
    return localStorage.getItem('token') || '';
  };

  get headers() {
    return {
      headers: {
        'x-token': this.token
      }
    }
  }

  search(
    collection: 'USERS'|'TRANSPORTS'|'LOOKUPCODEGROUPS'|'LOOKUPCODES',
    term: string,

    ) {
    const url = `${base_url}/search/collection/${collection}/${term}`;
    return this.http.get<any[]>(url, this.headers)
            .pipe(
              map((resp: any) => resp.result),
              catchError(error => {
                return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
              })              
            );
  }

  searchAll(term: string) {
    const url = `${base_url}/search/all/${term}`;
    return this.http.get<any[]>(url, this.headers)
            .pipe(
              map((resp: any) => resp.result),
              catchError(error => {
                return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
              })     
            );
  }
}
