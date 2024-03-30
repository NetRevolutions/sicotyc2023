import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ValidationErrorsCustomizeService {

  constructor(
    private router: Router
  ) { }

  public messageCatchError(error: any): string {
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
      errorMessage = 'No se encuentra autorizado por el token o este ha expirado';
      this.router.navigateByUrl('/login');
    }
    return errorMessage;
  }
}
