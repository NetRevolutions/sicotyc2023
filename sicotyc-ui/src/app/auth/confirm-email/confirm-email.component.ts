import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';

// Services
import { UserService } from 'src/app/services/user.service';
import { IResultProcess } from 'src/app/interfaces/result-process.interface';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.development';

const ui_url = environment.ui_url;

@Component({
  selector: 'app-confim-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: [ './confirm-email.component.css' ]
})
export class ConfirmEmailComponent implements OnInit{
  public code?: string;
  public id?: string;

  constructor(
    private userService: UserService,
    private router: Router
  ) {};
  
  ngOnInit(): void {
    const urlParams = new URLSearchParams(window.location.search);
    if (urlParams.get('code') != null && urlParams.get('id') != null) {
      this.code = decodeURIComponent(urlParams.get('code')?.toString() ?? '');
      this.id = decodeURIComponent(urlParams.get('id')?.toString() ?? '');
      this.userService.validateUserActivation(this.code, this.id)
      .subscribe({
        next: (resp: IResultProcess) => {
          if (resp.success == true) {
            Swal.fire("Exito", resp.message, 'success')
            .then((result) => {
              if (result.isConfirmed) {
                this.router.navigateByUrl('/login');
              }              
            })
          }
        },
        error: (err) => {
          Swal.fire('Error', err.message, 'error');
        },
        complete: () => console.info('activacion de usuario exitosa!!!')
      })
    }
    else {
      Swal.fire("Error", "Error al confirmar el correo electronico del usuario, cierre el navegador y contacte al administrador", "error");
    }
  };

}
