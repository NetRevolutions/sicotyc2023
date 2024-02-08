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

  public user!: User;

  constructor(private http: HttpClient,
              private router: Router) { }

  createUser( formData: RegisterForm) {    
    return this.http.post(`${base_url}/authentication`, formData);    
  };

  loginUser( formData: LoginForm) {
    return this.http.post(`${base_url}/authentication/login`, formData)
                .pipe(
                  tap((resp: any) => {
                    localStorage.setItem('token', resp.token);
                  })
                )
  };

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('claims');
    this.router.navigateByUrl('/login');
  }

  getClaims(): any {
    const token = localStorage.getItem('token') || '';
    return this.http.get(`${base_url}/authentication/claims?token=${token}`);    
  };

  getClaimsFromLocalStorage(): any {
    const claims = localStorage.getItem('claims') || '';
    return JSON.parse(claims);
  }

  validateToken(): Observable<boolean> {
    const token = localStorage.getItem('token') || '';
    return this.http.get(`${base_url}/authentication/renewToken`, {
      headers: {
        'x-token': token
      }
    }).pipe(
      tap( (resp: any) => {
        const { firstName, lastName, userName, email, img, id } = resp.user;
        this.user = new User(firstName, lastName, userName, email, '', img, resp.roles, id);

        localStorage.setItem('token', resp.token); // Seteamos el token renovado
        
      }),
      map( resp => true),
      catchError(error => of(false)) // Con el of retorno un Observable de tipo boolean (false)
    );
  };

}
