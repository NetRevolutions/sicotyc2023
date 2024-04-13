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
    if (error.status === 404) // not Found
    {
      errorMessage = 'No se encontraron datos';
      this.router.navigateByUrl('/error-404');
    } 
    else if (error.status === 500) // Internal Server Error
    {
      errorMessage = 'Error interno del servidor';
      this.router.navigateByUrl('/error-500');
    }
    else if (error.status === 400) // BadRequest
    {
      if (error.error.message !== undefined || error.error.message !== '')
      {
        errorMessage = error.error.message;
      }
    }
    else if (error.status === 401) // Unauthorized
    {
      errorMessage = 'No se encuentra autorizado por el token o este ha expirado';
      this.router.navigateByUrl('/login');
    }
    return errorMessage;
  }
}
