import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { delay, map } from 'rxjs/operators';
import Swal from 'sweetalert2';

// Enums
import { EnumLookupCodeGroups } from 'src/app/enum/enums.enum';

// Interfaces
import { ILookupCode } from 'src/app/interfaces/lookup.interface';

// Models
import { User } from 'src/app/models/user.model';
import { UserDetail } from 'src/app/models/user-detail.model';

// Services
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
  };

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

    // this.lookupService.getTypeOfDocuments()
    // .subscribe({
    //   next: (resp) => {console.log(resp.data);},
    //   error: (err) => {console.error('ups, ocurrio un error', err);},
    //   complete: () => console.info('Se obtuvo los tipos de documentos satisfactoriamente')
    // })
  };

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

      const { email, userName, firstName, lastName, ruc, userDetail } = user.data; // con esto destructuro y paso los valores que necesito
      this.userSelected = user.data;
      // console.log('userSelected', this.userSelected);
      this.userForm.setValue({email, userName, firstName, lastName, ruc });
      if (userDetail != null)
      {
        const fecha = this.convertirFecha(userDetail.dateOfBirth == null ? new Date() : new Date(userDetail.dateOfBirth));

        this.userDetailForm.setValue({
          dateOfBirth: fecha, 
          address: userDetail.address, 
          documentType: userDetail.documentType, 
          documentNumber: userDetail.documentNumber
        });
      }
      return false;
    });
  };

  saveUser(){
    let userDetail: UserDetail = {
      ...this.userDetailForm.value,
      id: this.userSelected?.id,
      createdBy: this.userService.uid
    };

    let userData: User = {
      ...this.userForm.value,
      id: this.userSelected?.id,      
      userDetail: userDetail,
      roles: this.userSelected?.roles
    };
    
    //console.log('userData', userData);
    this.userService.updateUser(userData)
    .subscribe({
      next: (resp) => { 

        Swal.fire("Exito", resp.toString(), 'success');
      },
      error: (err) => {
        Swal.fire('Error', err.error.msg, 'error');
      },
      complete: () => console.info('Usuario actualizado')
    });    
  };

  loadTypeOfDocuments() {
    this.lookupService.getLookupCodesByLCGNameALL(EnumLookupCodeGroups.TIPO_DOCUMENTO)
    .subscribe(resp => {
      this.typeOfDocuments = resp.data;
    });    
  };

  convertirFecha(fecha: Date): string {
    const anio = fecha.getFullYear();
    const mes = ('0' + (fecha.getMonth() + 1)).slice(-2);
    const dia = ('0' + fecha.getDate()).slice(-2);

    return `${anio}-${mes}-${dia}`;
  };
}
