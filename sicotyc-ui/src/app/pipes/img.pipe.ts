import { Pipe, PipeTransform } from '@angular/core';
import { environment } from 'src/environments/environment.development';

const base_url = environment.base_url;

@Pipe({
  name: 'img'
})
export class ImgPipe implements PipeTransform {

  transform(img?: string, type?: 'users'|'transports'|'drivers'): string {
    if ( !img) {
      return `${ base_url }/upload/users/no-image`;
    } else if ( img ) {
      return `${ base_url }/upload/${ type }/${ img }`
    } else {
      return `${ base_url }/upload/users/no-image`;
    }
  }
}
