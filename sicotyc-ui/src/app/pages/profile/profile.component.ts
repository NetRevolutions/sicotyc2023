import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import Swal from 'sweetalert2';

import { User } from 'src/app/models/user.model';

// Services
import { UserService } from 'src/app/services/user.service';
import { FileUploadService } from 'src/app/services/file-upload.service';
import { leadingComment } from '@angular/compiler';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styles: [
  ]
})
export class ProfileComponent implements OnInit{
  public profileForm: FormGroup;
  public user?: User;
  public imgUpload?: File;
  public imgTemp: any;
  
  constructor(private fb: FormBuilder,
              private userService: UserService,
              private fileUploadService: FileUploadService) {
    this.user = userService.user;
    this.profileForm = this.fb.group({}); 
  }
  
  ngOnInit(): void {
    
    this.profileForm = this.fb.group({
      userName: [this.user?.userName, Validators.required ],
      email: [this.user?.email, [Validators.required, Validators.email]]
    });
  };

  updateProfile() {
    // console.log(this.profileForm.value);
    this.userService.updateProfile(this.profileForm.value)
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
}
