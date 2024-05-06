import { Component } from '@angular/core';

// Interface
import { IMenuItem } from 'src/app/interfaces/menu-item.interface';

// Models
import { User } from 'src/app/models/user.model';

// Services
import { SidebarService } from 'src/app/services/sidebar.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styles: [
  ]
})
export class SidebarComponent {
  public user?: User;

  constructor( 
    public sideService: SidebarService,
    private userService: UserService
    ) {
    this.user = userService.user;    
  }

}
