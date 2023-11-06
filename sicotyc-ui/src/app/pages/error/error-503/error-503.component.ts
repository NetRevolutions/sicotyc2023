import { Component } from '@angular/core';

@Component({
  selector: 'error-503',
  templateUrl: './error-503.component.html',
  styleUrls: ['./error-503.component.css']
})
export class Error503Component {
  year = new Date().getFullYear();
}
