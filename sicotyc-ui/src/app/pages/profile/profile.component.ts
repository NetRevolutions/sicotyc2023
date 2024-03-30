import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import Swal from 'sweetalert2';

// Models
import { User } from 'src/app/models/user.model';
import { UserDetail } from 'src/app/models/user-detail.model';

// Interfaces
import { ILookupCode } from 'src/app/interfaces/lookup.interface';

// Enums
import { EnumLookupCodeGroups } from 'src/app/enum/enums.enum';

// Services
import { UserService } from 'src/app/services/user.service';
import { FileUploadService } from 'src/app/services/file-upload.service';
import { LookupService } from 'src/app/services/lookup.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styles: [
  ]
})
export class ProfileComponent implements OnInit{
  public profileForm: FormGroup;
  public userDetailForm: FormGroup;
  public user?: User;
  public imgUpload?: File;
  public imgTemp: any;
  public typeOfDocuments: ILookupCode[] = [];
  
  constructor(private fb: FormBuilder,
              private userService: UserService,
              private fileUploadService: FileUploadService,
              private lookupService: LookupService) {
    this.user = userService.user;
    this.profileForm = this.fb.group({}); 
    this.userDetailForm = this.fb.group({});
  }
  
  ngOnInit(): void {
    this.loadTypeOfDocuments();

    this.loadUserProfile();
  };

  loadUserProfile() {
    this.profileForm = this.fb.group({
      userName: [this.user?.userName, Validators.required ],
      email: [this.user?.email, [Validators.required, Validators.email]],
      //firstName: [this.user?.firstName, [Validators.required, Validators.minLength(3)]],
      // lastName: [this.user?.lastName, [Validators.required, Validators.minLength(3)]],
      // ruc: [this.user?.ruc, [Validators.required, Validators.minLength(11), Validators.maxLength(11)]]
      firstName: [{value: this.user?.firstName, disabled: true}], // Campo de Solo lectura
      lastName: [{value: this.user?.lastName, disabled: true}], // Campo de Solo lectura
      ruc: [{value: this.user?.ruc, disabled: true}] // Campo de Solo lectura
    });
    // TODO: Si mas adelante hubiera un cambio de ruc, se tendria que validar si ya existe un administrador para ese ruc
    // y tendria que esperar a que el administrador le de permiso para acceder a ese ruc.

    const fecha = this.convertirFecha(this.user?.userDetail?.dateOfBirth == null ? new Date() : new Date(this.user?.userDetail?.dateOfBirth));

    this.userDetailForm = this.fb.group({
      dateOfBirth: [fecha, Validators.required],
      address: [this.user?.userDetail?.address],
      documentType: [this.user?.userDetail?.documentType, Validators.required],
      documentNumber: [this.user?.userDetail?.documentNumber, Validators.required]
    });
  }

  updateProfile() {    
    let userDetail: UserDetail = {
      ...this.userDetailForm.value,
      id: this.user?.id,
      createdBy: this.user?.id
    };

    let userData: User = {
      ...this.profileForm.value,
      id: this.user?.id,
      firstName: this.user?.firstName,
      lastName: this.user?.lastName,
      userDetail: userDetail,
      roles: this.user?.roles,
      ruc: this.user?.ruc
    };

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

  changeImage(file: File) {
    this.imgUpload = file;

    if (!file) { 
      this.imgTemp = null; 
      return;
    }

    const reader = new FileReader();
    reader.readAsDataURL( file );

    reader.onloadend = () => {
      this.imgTemp = reader.result?.toString();         
    }
  };

  uploadImage() {
    this.fileUploadService
      .updatePhoto(this.imgUpload!, 'USERS', this.user?.id!)
      .then( img => {             
        this.user!.img = img;
        Swal.fire("Exito", 'Imagen de usuario actualizada', 'success');    
      })
      .catch( err => {
        console.log(err);
        Swal.fire('Error', 'No se pudo subir la imagen', 'error');
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
  }
}
