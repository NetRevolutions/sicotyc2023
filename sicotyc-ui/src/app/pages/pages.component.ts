import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../services/settings.service';
import { SidebarService } from '../services/sidebar.service';

// Con esto indico a Angular confie en esta funcion global
declare function customInitFunctions(): void;

@Component({
  selector: 'app-pages',
  templateUrl: './pages.component.html',
  styles: [
  ]
})
export class PagesComponent implements OnInit {
  year = new Date().getFullYear();
  

  constructor( private settingsService: SettingsService,
                private sidebarService: SidebarService) {}
  
  
  ngOnInit(): void {
    customInitFunctions();
    this.sidebarService.loadMenu();
    
  }

}
