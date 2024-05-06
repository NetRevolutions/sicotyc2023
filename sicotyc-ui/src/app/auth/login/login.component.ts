import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: [ './login.component.css' ]
})
export class LoginComponent implements OnInit{

  public formSubmitted: boolean = false;

  public loginForm: FormGroup = new FormGroup({
    userName: new FormControl(localStorage.getItem('username') || '', Validators.required),
    password: new FormControl('', Validators.required),
    remember: new FormControl(false)
  });
  
  constructor( private router: Router,
                private userService: UserService) {}
  
  ngOnInit(): void {
  
  }
  
  login() {
    this.formSubmitted = true;
    if (this.loginForm.invalid) return;

    this.userService.loginUser(this.loginForm.value)
    .subscribe({
      next: (result) => {
        if (this.loginForm.get('remember')?.value) {
          localStorage.setItem('username', this.loginForm.get('userName')?.value);
        } else {
          localStorage.removeItem('username');
        };        
        this.userService.getClaims()
        .subscribe((result: any) => {           
          // Seteamos los valores en el local storage
          //console.log('result', result);
          if (result.claims != null) {
            localStorage.setItem('claims', JSON.stringify(result.claims));
          };          
        })
        //Navegamos al Dashboard
        this.router.navigateByUrl('/');
      },
      error: (err) => {
        Swal.fire('Error', err.message, 'error');
      },
      complete: () => console.info('login de usuario realizado')
    })

    
  }
}
