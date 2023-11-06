import { Component } from '@angular/core';

@Component({
  selector: 'error-403',
  templateUrl: './error-403.component.html',
  styleUrls: ['./error-403.component.css']
})
export class Error403Component {
  year = new Date().getFullYear();
}
