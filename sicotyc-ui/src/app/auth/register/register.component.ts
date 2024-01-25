import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

import { fnpasswordsIguales } from 'src/app/directives/fnpasswordsIguales.directive';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: [ './register.component.css' ]
})
export class RegisterComponent {

  public formSubmitted: boolean = false;

  public registerForm: FormGroup =  new FormGroup({//this.fb.group({
    firstName: new FormControl('', [Validators.required, Validators.minLength(3)]),
    lastName: new FormControl('', [Validators.required, Validators.minLength(3)]),
    userName: new FormControl('', [Validators.required, Validators.minLength(3)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('',[Validators.required, Validators.minLength(10)]),
    password2: new FormControl('', [Validators.required, Validators.minLength(10)]),
    terms: new FormControl(false, [Validators.required]),
    // roles: ['Administrator']
  }, {
    validators: fnpasswordsIguales
  });

  constructor(//private fb: FormBuilder,
              private userService: UserService,
              private router: Router) { };

  createUser() {
    this.formSubmitted = true;
    //console.log(this.registerForm.value);

    if ( this.registerForm.invalid || 
         this.registerForm.get('terms')?.value == null || 
         this.registerForm.get('terms')?.value == false ) {
      return;
    } 

    // Realizar el posteo
    this.userService.createUser(this.registerForm.value)
    .subscribe({
      next: (valor) => {
        //console.log('usuario creado')
        Swal.fire('Exito!!', 'El usuario fue creado', 'success')
        .then((result) => {
          if (result.isConfirmed){
            this.formSubmitted = false;
            this.router.navigateByUrl('/login');
          }
        });
      },
      error: (err) => {
        //console.warn( err )
        Swal.fire('Error', err.error.msg, 'error');
      },
      complete: () => console.info('servicio de registro culminado')
    });
    
  };

  fieldNotValidate(field: string): boolean 
  {
    if ( this.registerForm.get(field)?.invalid && this.formSubmitted ) {
      return true;
    } else {
      return false;
    }
  };

  passwordNotValidate()
  {
    const pass1 = this.registerForm.get('password')?.value;
    const pass2 = this.registerForm.get('password2')?.value;

    if ( pass1 !== pass2 && this.formSubmitted ) {
      return true;
    } else {
      return false;
    }
  };

  passwordsEmpty()
  {
    const pass1 = this.registerForm.get('password')?.value;
    const pass2 = this.registerForm.get('password2')?.value;

    if ((!pass1 || pass1.trim() === '' || !pass2 || pass2.trim() === '') && 
        this.formSubmitted) {
      return true;
    } else {
      return false;
    }
  };  

  acceptTerms() {
    return !this.registerForm.get('terms')?.value && this.formSubmitted;
  };

  enableButton(): boolean {
    if ( this.registerForm.invalid || 
      this.registerForm.get('terms')?.value == null || 
      this.registerForm.get('terms')?.value == false ) {
      return true;
    }

    return false;
  }

  // passwordsEquals(pass1Name: string, pass2Name: string) {
  //   return (formGroup: FormGroup) => {
  //     const pass1Control = formGroup.get(pass1Name);
  //     const pass2Control = formGroup.get(pass2Name);

  //     if (pass1Control?.value === pass2Control?.value)
  //     {
  //       pass2Control?.setErrors(null);
  //     } else {
  //       pass2Control?.setErrors({ notEqual: true});
  //     }
  //   };
  // };  

}
