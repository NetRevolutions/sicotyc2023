import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { catchError, map, tap } from "rxjs/operators";
import { Observable, of, throwError } from 'rxjs';

import { environment } from 'src/environments/environment.development';

import { RegisterForm } from '../interfaces/register-form.interface';
import { LoginForm } from '../interfaces/login-form.interface';

import { User } from '../models/user.model';
import { IPagination } from '../interfaces/pagination.interface';

const base_url = environment.base_url;


@Injectable({
  providedIn: 'root'
})
export class UserService {

  public user?: User;
  
  constructor(private http: HttpClient,
              private router: Router) { }

  get token(): string {

    return localStorage.getItem('token') || '';
  };

  get uid(): string {
    return this.user?.id || '';
  };

  get roles(): string[] {
    return this.user?.roles || [];
  }

  get headers() {
    return {
      headers: {
        'x-token': this.token
      }
    }
  }

  createUser( formData: RegisterForm) {    
    return this.http.post(`${base_url}/authentication`, formData)
    .pipe(
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
  };

  loginUser( formData: LoginForm) {
    return this.http.post(`${base_url}/authentication/login`, formData)
                .pipe(
                  tap((resp: any) => {
                    localStorage.setItem('token', resp.token);
                    //localStorage.setItem('companyId', resp.companyId);
                  }),
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
                )
  };

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('claims');
    //localStorage.removeItem('companyId');
    this.router.navigateByUrl('/login');
  }

  getClaims(): any {
    const token = localStorage.getItem('token') || '';
    return this.http.get(`${base_url}/authentication/claims?token=${token}`)
    .pipe(
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
  };

  getClaimsFromLocalStorage(): any[] {
    const claims = localStorage.getItem('claims') || '';
    return JSON.parse(claims);
  }

  validateToken(): Observable<boolean> {    
    return this.http.get(`${base_url}/authentication/renewToken`, this.headers)
    .pipe(
      map( (resp: any) => {
        const { firstName, lastName, userName, email, phoneNumber, img = '', id, ruc, userDetail } = resp.user;
        this.user = new User(firstName, lastName, userName, email, '', img, phoneNumber, resp.roles, id, ruc, userDetail);

        localStorage.setItem('token', resp.token); // Seteamos el token renovado
        return true;
      }),     
      catchError(error => of(false)) // Con el of retorno un Observable de tipo boolean (false)
    );
  };

  
  updateUser( data: User) {
    const url = `${base_url}/authentication/user/${this.uid}`;
    return this.http.put(url, data, this.headers)
    .pipe(
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
  };

  loadUsers( pagination: IPagination) {
    const url = `${base_url}/authentication/users?pageNumber=${pagination.pageNumber}&pageSize=${pagination.pageSize}`;
    return this.http.get(url, this.headers)
      .pipe(
        map((resp: any) => {
          const users = resp.data.map(
            (user: User) => new User(user.firstName, user.lastName, user.userName, user.email, '', user.img, user.phoneNumber, user.roles, user.id, user.ruc, user.userDetail));
          
          return { 
            data: users, 
            pagination: resp.pagination
          };
        }),
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
      )
  };

  findUsersByIdCollection(ids: string[]) {
    const url = `${base_url}/authentication/users/collection(${ids.join(',')})`;
    return this.http.get(url, this.headers)
      .pipe(
        map((resp: any) => {
          const users = resp.data.map(
            (user: User) => new User(user.firstName, user.lastName, user.userName, user.email, '', user.img, user.phoneNumber, user.roles, user.id, user.ruc, user.userDetail));
          
          return {
            data: users
          };
        }),
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
      )
  };

  getUserById(id: string) {
    const url = `${base_url}/authentication/user/${id}`
    return this.http.get(url, this.headers)
      .pipe(
        map((resp: any) => {      
          const { firstName, lastName, userName, email, phoneNumber, img = '', roles, id, ruc, userDetail } = resp.data;    
          const user = new User(firstName, lastName, userName, email, '', img, phoneNumber, roles, id, ruc, userDetail);
          return {
            data: user
          };
        }),
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

  deleteUser(user: User) {
    // console.log('User eliminado');
    const url = `${base_url}/authentication/user/${user.id}`;
    return this.http.delete(url, this.headers)
    .pipe(
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
    )
  }
}
