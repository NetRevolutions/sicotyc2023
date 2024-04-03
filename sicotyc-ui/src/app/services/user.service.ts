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
import { ValidationErrorsCustomizeService } from './validation-errors-customize.service';
import { IChangePassword } from '../interfaces/changePassword.interface';

const base_url = environment.base_url;


@Injectable({
  providedIn: 'root'
})
export class UserService {

  public user?: User;
  
  constructor(private http: HttpClient,
              private router: Router,
              private validationErrorsCustomize: ValidationErrorsCustomizeService) { }

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
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
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
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
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
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
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
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
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
          return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
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
          return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
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
          return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
        })   
      );
  };

  deleteUser(user: User) {
    // console.log('User eliminado');
    const url = `${base_url}/authentication/user/${user.id}`;
    return this.http.delete(url, this.headers)
    .pipe(
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    )
  };

  changePassword(formData: IChangePassword) {
    const data = {
      ...formData,
      id: this.uid
    };
    
    const url = `${base_url}/authentication/change-password`;
    return this.http.post(url, data, this.headers)
    .pipe(
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })
    )
  };
}
