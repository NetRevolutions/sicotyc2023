import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-terms-conditions-popup',
  templateUrl: './terms-conditions-popup.component.html',
  styleUrls: ['./terms-conditions-popup.component.css']
})
export class TermsConditionsPopupComponent {

  @Output() closePopupEvent = new EventEmitter();

  closePopup() {
    this.closePopupEvent.emit();
  }
}
