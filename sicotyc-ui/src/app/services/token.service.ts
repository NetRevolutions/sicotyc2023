import { Injectable } from '@angular/core';
// import * as jwt from 'jsonwebtoken';
import { environment } from 'src/environments/environment.development';

const token = JSON.parse(localStorage.getItem('token') || '{}');
const secretKey = environment.secret_key;

@Injectable({
  providedIn: 'root'
})
export class TokenService { 

  constructor() { }

  getClaims(): any {
    try {

      // console.log(token);
      // // Decodificar el token
      // const decodedToken: any = jwt.verify(token, secretKey);

      // // Acceder a los Claims del token
      // const claims = decodedToken ? decodedToken : null;

      // return claims;
      
    } catch (error: any) {
      console.error('Error al decodificar el token', error.message);
    }
  }
}
