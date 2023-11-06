import { Component } from '@angular/core';

@Component({
  selector: 'error-500',
  templateUrl: './error-500.component.html',
  styleUrls: [ './error-500.component.css' ]
})
export class Error500Component {
  year = new Date().getFullYear();
}
