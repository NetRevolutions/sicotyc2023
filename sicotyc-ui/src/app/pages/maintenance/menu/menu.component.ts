import { Component, OnInit } from '@angular/core';
import { IMenuOption } from 'src/app/interfaces/menu-options.interface';
import { ITuple } from 'src/app/interfaces/tuple.interface';
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styles: [
  ]
})
export class MenuComponent implements OnInit{
  public roleIdSelected: string = ''
  public loading: boolean = true;
  public roles: ITuple[] = [];
  public menuOptions: IMenuOption[] = [];

  constructor(
     private userService: UserService
  ) {}


  ngOnInit(): void {
    this.loadRoles();
  };  

  loadRoles() {
    this.loading = true;
    this.userService.getRolesForManintenance()
    .subscribe((resp: any) => {
      this.roles = resp.roles;
      this.loading = false;
    })
  };

  onChangeRole(event: any) {
    this.roleIdSelected = event.target.value;
    if (this.roleIdSelected !== '') {
      this.loadMenuByRole();
    }
    else {
      this.menuOptions = [];
    }
    
  };

  loadMenuByRole() {
    this.loading = true;
    this.userService.getMenuOptionsByRole(this.roleIdSelected)
    .subscribe((resp: any) => {
      this.menuOptions = this.sortMenuOptions(resp.menu);
      //console.log(this.menuOptions);
      this.loading = false;
    })
  };

  sortMenuOptions(menuOption: IMenuOption[]): IMenuOption[] {
    let menuOptionsSorted: IMenuOption[] = [];
    let menuOptionsParent: IMenuOption[] = menuOption.filter(f => f.optionLevel == 1);

    menuOptionsParent.forEach(mp => {
      let menuOptionsChild: IMenuOption[] = menuOption.filter(f => f.optionParentId == mp.optionId && f.optionLevel == 2)
                                                      .sort((a,b) => ((a.optionOrder ?? 0) > (b.optionOrder ?? 0)) ? 1 : -1); 
      if (menuOptionsChild.length > 0)
      {
        // Has children, insert parent
        menuOptionsSorted.push({  
          optionId : mp.optionId,
          title: mp.title,
          icon: mp.icon,
          url: mp.url,
          optionOrder: mp.optionOrder,
          optionLevel: mp.optionLevel,
          optionParentId: mp.optionParentId,
          isEnabled: true // mp.isEnabled
        });

        // Insert children
        menuOptionsChild.forEach(mc => {
          menuOptionsSorted.push({
            optionId : mc.optionId,
            title: mc.title,
            icon: mc.icon,
            url: mc.url,
            optionOrder: mc.optionOrder,
            optionLevel: mc.optionLevel,
            optionParentId: mc.optionParentId,
            isEnabled: mc.isEnabled
          })
        });
      }
    });

    return menuOptionsSorted;
  };

  saveOptionsByRole() {    
    let menuOptionIds: string[] = [];
    this.menuOptions.forEach(m => {
      if (m.isEnabled == true) {
        menuOptionIds.push(m.optionId ?? '');
      }      
    });

    this.userService.updateMenuoptionsByRole(this.roleIdSelected, menuOptionIds)
    .subscribe({
      next: (resp: any) => {
        if (resp.menuOptions != undefined && resp.menuOptions.length > 0)
        {
          Swal.fire({
            title: 'Exito!',
            text: 'Las opciones del menu para este rol fueron actualizados',
            icon: "success"
          });
        }
        else {
          Swal.fire({
            title: 'Informacion!',
            text: 'No se realizo ningun cambios en las opciones del menu para este role',
            icon: "info"
          });
        }
      },
      error: (err) => {
        Swal.fire('Error', 'Hubo un error durante el proceso de eliminacion, verifique!!!', 'error');
      },
      complete: () => {
        console.info(`La opciones de menu para el rol ${this.roleIdSelected} fue actualizado`);
      }
    });
  };

}
