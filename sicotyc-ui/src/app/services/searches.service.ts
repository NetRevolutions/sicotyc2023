import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment.development';

const base_url = environment.base_url;

@Injectable({
  providedIn: 'root'
})
export class SearchesService {

  constructor(
    private http: HttpClient,
    private router: Router
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
                let errorMessage = 'Ha ocurrido un error.';
                if (error.status === 404)
                {
                  errorMessage = 'No se encontraron datos';
                } 
                else if (error.status === 500)
                {
                  errorMessage = 'Error interno del servidor';
                }
                else if (error.status === 401)
                {
                  errorMessage = 'No se encuentra autorizado por el token';
                  this.router.navigateByUrl('/login');
                }
                return throwError(() => new Error(errorMessage));
              })              
            );
  }

  searchAll(term: string) {
    const url = `${base_url}/search/all/${term}`;
    return this.http.get<any[]>(url, this.headers)
            .pipe(
              map((resp: any) => resp.result),
              catchError(error => {
                let errorMessage = 'Ha ocurrido un error.';
                if (error.status === 404)
                {
                  errorMessage = 'No se encontraron datos';
                } 
                else if (error.status === 500)
                {
                  errorMessage = 'Error interno del servidor';
                }
                else if (error.status === 401)
                {
                  errorMessage = 'No se encuentra autorizado por el token';
                  this.router.navigateByUrl('/login');
                }
                return throwError(() => new Error(errorMessage));
              })     
            );
  }
}
