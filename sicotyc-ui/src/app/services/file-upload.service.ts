import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';


const base_url = environment.base_url;

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {

  constructor() { }

  async updatePhoto (
    imgFile: File,
    folderFile: 'USERS'|'TRANSPORTS',
    id: string
  ) {

    try {

      const url = `${ base_url }/upload/${ folderFile }/${ id }`
      const formData = new FormData();
      formData.append('imagen', imgFile);

      const resp = await fetch( url, {
        method: 'PUT',
        headers: {
          'x-token': localStorage.getItem('token') || ''
        },
        body: formData
      });

      const data = await resp.json();
      if (data != null && data.fileName) {
        return data.fileName
      } else {
        return false;
      }      
      
    } catch (error) {
      console.log(error);
      return false;
    }
  }
}
