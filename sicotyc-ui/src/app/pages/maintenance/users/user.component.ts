import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { delay } from 'rxjs/operators';
import { EnumLookupCodeGroups } from 'src/app/enum/enums.enum';
import { ILookupCode } from 'src/app/interfaces/lookup.interface';

import { User } from 'src/app/models/user.model';

import { LookupService } from 'src/app/services/lookup.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styles: [
  ]
})
export class UserComponent implements OnInit{
  public userForm: FormGroup;
  public userDetailForm: FormGroup;
  public userSelected?: User;
  public typeOfDocuments: ILookupCode[] = [];

  constructor(private fb: FormBuilder,
              private userService: UserService,
              private lookupService: LookupService,
              private router: Router,
              private activatedRoute: ActivatedRoute) {

    this.userForm = this.fb.group({});
    this.userDetailForm = this.fb.group({});    
  }

  ngOnInit(): void {

    this.activatedRoute.params
      .subscribe( ({ id }) => {
        this.loadUser( id );
        this.loadTypeOfDocuments();
      });
    
    this.userForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      userName: ['', [Validators.required, Validators.minLength(3)]],
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      ruc: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]]
    });

    this.userDetailForm = this.fb.group({
      dateOfBirth: [''],
      address: [''],
      documentType: [''],
      documentNumber: ['']
    });
  }

  loadUser(id: string) {
    if (id === 'new') {
      return;
    }

    this.userService.getUserById( id )
    .pipe(
      delay(100)
    )
    .subscribe( user => {
      if ( !user.data ) {
        return this.router.navigateByUrl(`/mantenimientos/users`);
      }

      const { email, userName, firstName, lastName, ruc } = user.data; // con esto destructuro y paso los valores que necesito
      this.userSelected = user.data;
      this.userForm.setValue({email, userName, firstName, lastName, ruc });
      return false;
    });
  }

  saveUser(){
    console.log('pending');
  }

  loadTypeOfDocuments() {
    this.lookupService.getLookupCodesByLCGNameALL(EnumLookupCodeGroups.TIPO_DOCUMENTO)
    .subscribe(resp => {
      this.typeOfDocuments = resp.data;
    });    
  }
}
