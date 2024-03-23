import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { catchError, map, tap } from "rxjs/operators";
import { Observable, of } from 'rxjs';

import { environment } from 'src/environments/environment.development';

import { RegisterForm } from '../interfaces/register-form.interface';
import { LoginForm } from '../interfaces/login-form.interface';

import { User } from '../models/user.model';

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
    return this.http.post(`${base_url}/authentication`, formData);    
  };

  loginUser( formData: LoginForm) {
    return this.http.post(`${base_url}/authentication/login`, formData)
                .pipe(
                  tap((resp: any) => {
                    localStorage.setItem('token', resp.token);
                    //localStorage.setItem('companyId', resp.companyId);
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
    return this.http.get(`${base_url}/authentication/claims?token=${token}`);    
  };

  getClaimsFromLocalStorage(): any[] {
    const claims = localStorage.getItem('claims') || '';
    return JSON.parse(claims);
  }

  validateToken(): Observable<boolean> {    
    return this.http.get(`${base_url}/authentication/renewToken`, this.headers)
    .pipe(
      map( (resp: any) => {
        const { firstName, lastName, userName, email, phoneNumber, img = '', id, ruc } = resp.user;
        this.user = new User(firstName, lastName, userName, email, '', img, phoneNumber, resp.roles, id, ruc);

        localStorage.setItem('token', resp.token); // Seteamos el token renovado
        return true;
      }),     
      catchError(error => of(false)) // Con el of retorno un Observable de tipo boolean (false)
    );
  };

  
  updateProfile( data: User
                      // {
                      //   id: string, 
                      //   firstName: string,
                      //   lastName: string,
                      //   userName: string,// No actualizar
                      //   email: string,  
                      //   phoneNumber: string,
                      //   roles: Array<string>,
                      //   ruc: string
                      // }
                      ) {   
    /*                   
    data.id = this.uid;
    data.firstName = this.user?.firstName || '';
    data.lastName = this.user?.lastName || '';
    //data.userName = this.user?.userName || ''; // No actualizar
    //data.email = this.user?.email || '';
    data.phoneNumber = this.user?.phoneNumber || '';
    data.roles = this.user?.roles || []; // No Actualizar
    data.ruc = this.user?.ruc || ''; 
    */
    return this.http.put(`${base_url}/authentication/user/${this.uid}`, data, this.headers)
  };

  loadUsers( from: number = 0) {
    const url = `${base_url}/authentication/users?pageNumber=${from}`;
    return this.http.get(url, this.headers)
      .pipe(
        map((resp: any) => {
          const users = resp.data.map(
            (user: User) => new User(user.firstName, user.lastName, user.userName, user.email, '', user.img, user.phoneNumber, user.roles, user.id, user.ruc));
          
          return { 
            data: users, 
            pagination: resp.pagination
          };
        })
      )
  };

  findUsersByIdCollection(ids: string[]) {
    const url = `${base_url}/authentication/users/collection(${ids.join(',')})`;
    return this.http.get(url, this.headers)
      .pipe(
        map((resp: any) => {
          const users = resp.data.map(
            (user: User) => new User(user.firstName, user.lastName, user.userName, user.email, '', user.img, user.phoneNumber, user.roles, user.id, user.ruc));
          
          return {
            data: users
          };
        })
      )
  };

  getUserById(id: string) {
    const url = `${base_url}/authentication/user/${id}`
    return this.http.get(url, this.headers)
      .pipe(
        map((resp: any) => {          
          const user = new User(resp.data.firstName, resp.data.lastName, resp.data.userName, resp.data.email, '', resp.data.img, resp.data.phoneNumber, resp.data.roles, resp.data.id, resp.data.ruc);
          return {
            data: user
          };
        })
      );
  }

  deleteUser(user: User) {
    // console.log('User eliminado');
    const url = `${base_url}/authentication/user/${user.id}`;
    return this.http.delete(url, this.headers)
  }
}
