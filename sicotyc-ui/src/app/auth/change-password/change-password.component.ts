import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { fnpasswordsIguales } from 'src/app/directives/fnpasswordsIguales.directive';
import { IChangePassword } from 'src/app/interfaces/change-password.interface';

// Services
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: [ '../login/login.component.css']
})
export class ChangePasswordComponent implements OnInit{
  public loginForm: FormGroup;
  public formSubmitted: boolean = false;

  constructor(
    private location: Location,    
    private router: Router,
    private userService: UserService
  ) {
    this.loginForm = new FormGroup({});
  }
  
  ngOnInit(): void {
    this.loginForm = new FormGroup({
      currentPassword: new FormControl('', Validators.required),
      password: new FormControl('',[Validators.required, Validators.minLength(10)]),
      password2: new FormControl('', [Validators.required, Validators.minLength(10)]),
    }, {
      validators: fnpasswordsIguales
    });
  }

  changePassword() {
    this.formSubmitted = true;

    if (this.loginForm.invalid) { return; }

    let formData: IChangePassword = {
      oldPassword: this.loginForm.get('currentPassword')?.value,
      newPassword: this.loginForm.get('password')?.value
    }

    this.userService.changePassword(formData)
    .subscribe({
      next: (resp: any) => {
        Swal.fire('Exito!!', resp + 'Debe de volver a loguearse', 'success')
        .then((result) => {
          if (result.isConfirmed) {
            this.formSubmitted = false;
            this.router.navigateByUrl('/login');
          }
        });
      }, 
      error: (err) => {
        Swal.fire('Error', err.error.msg, 'error');
      },
      complete: () => console.info('Password cambiado exitosamente')
    });
  };

  goBack(): void {
    //window.history.back();
    this.location.back();
  };

  fieldNotValidate(field: string): boolean 
  {
    if ( this.loginForm.get(field)?.invalid && this.formSubmitted) {
      return true;
    } else {
      return false;
    }
  };

  passwordsEmpty()
  {
    const pass1 = this.loginForm.get('password')?.value;
    const pass2 = this.loginForm.get('password2')?.value;

    if ((!pass1 || pass1.trim() === '' || !pass2 || pass2.trim() === '') && this.formSubmitted) {
      return true;
    } else {
      return false;
    }
  }; 
}
