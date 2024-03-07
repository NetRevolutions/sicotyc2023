import { Component } from '@angular/core';
import { FileUploadService } from 'src/app/services/file-upload.service';
import { ModalImageService } from 'src/app/services/modal-image.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-modal-image',
  templateUrl: './modal-image.component.html',
  styles: [
  ]
})
export class ModalImageComponent {
  
  public imgUpload?: File;
  public imgTemp: any;

  constructor(public modalImageService: ModalImageService,
              public fileUploadService: FileUploadService) {}

  closeModal() {
    this.imgTemp = null;
    this.modalImageService.closeModal();
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
    const id = this.modalImageService.id;
    const folderFile = this.modalImageService.folderFile;

    this.fileUploadService
      .updatePhoto(this.imgUpload!, folderFile!, id!)
      .then( img => {   
        Swal.fire("Exito", 'Imagen de usuario actualizada', 'success'); 
        this.modalImageService.newImage.emit(img);
        this.closeModal();
      })
      .catch( err => {
        console.log(err);
        Swal.fire('Error', 'No se pudo subir la imagen', 'error');
      });
  };
}
