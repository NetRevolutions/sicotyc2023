import { Component } from '@angular/core';

@Component({
  selector: 'error-400',
  templateUrl: './error-400.component.html',
  styleUrls: ['./error-400.component.css']
})
export class Error400Component {
  year = new Date().getFullYear();
}
