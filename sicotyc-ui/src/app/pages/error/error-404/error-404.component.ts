import { Component } from '@angular/core';

@Component({
  selector: 'error-404',
  templateUrl: './error-404.component.html',
  styleUrls: [ './error-404.component.css' ]
})
export class Error404Component {
  year = new Date().getFullYear();
}
