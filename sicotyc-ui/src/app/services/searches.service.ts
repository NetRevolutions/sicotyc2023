import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment.development';

// Models
import { User } from '../models/user.model';


// Services
import { ValidationErrorsCustomizeService } from './validation-errors-customize.service';
import { ILookupCode, ILookupCodeGroup } from '../interfaces/lookup.interface';



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
  };

  private transformUsers(results: any[]): User[] {
    // return results.map(
    //   user => new User(user.firstName)
    // ); // TODO: Pending improve
    return results;
  };

  private transformTransports(results: any[]): any[] {
    return results;
  };

  private transformLookupCodeGroups(results: any): ILookupCodeGroup[] {
    return results;
  };

  private transformLookupCodes(results: any): ILookupCode[] {
    return results;
  }

  search(
    collection: 'USERS'|'TRANSPORTS'|'LOOKUPCODEGROUPS'|'LOOKUPCODES',
    term: string,

    ) {
    const url = `${base_url}/search/collection/${collection}/${term}`;
    return this.http.get<any[]>(url, this.headers)
    .pipe(
      map((resp: any) => {
        switch(collection){
          case 'USERS': 
            return this.transformUsers(resp);
          case 'TRANSPORTS':
            return this.transformTransports(resp);
          case 'LOOKUPCODEGROUPS':
            return this.transformLookupCodeGroups(resp);
          case 'LOOKUPCODES':
            return this.transformLookupCodes(resp);
          default:
            return [];
        };
      }),
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })              
    );
  };

  searchAll(term: string) {
    const url = `${base_url}/search/all/${term}`;
    return this.http.get<any[]>(url, this.headers)
            .pipe(
              map((resp: any) => resp.result),
              catchError(error => {
                return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
              })     
            );
  };
}
