import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from "rxjs/operators";

import { environment } from 'src/environments/environment.development';


import { RegisterForm } from '../interfaces/register-form.interface';
import { LoginForm } from '../interfaces/login-form.interface';

const base_url = environment.base_url;

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

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
  }
}
