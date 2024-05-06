import { Component, OnDestroy, OnInit } from '@angular/core';
import Swal from 'sweetalert2';

// Model
import { User } from 'src/app/models/user.model';

// Interfaces
import { IPagination } from 'src/app/interfaces/pagination.interface';

// Services
import { SearchesService } from 'src/app/services/searches.service';
import { UserService } from 'src/app/services/user.service';
import { ModalImageService } from 'src/app/services/modal-image.service';
import { Subscription, delay } from 'rxjs';
import { ITuple } from 'src/app/interfaces/tuple.interface';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styles: [
  ]
})
export class UsersComponent implements OnInit, OnDestroy{
  public users: User[] = [];
  public usersTemp: User[] = [];
  public pagination: IPagination = {
    pageNumber: 1,
    pageSize: 5,
    totalItems: 0
  };
  public loading: boolean = true;
  public useSearch: boolean = false;
  public imgSubs: Subscription = new Subscription();
  public roles: ITuple[] = [];

  constructor( public userService: UserService,
                private searchesService: SearchesService,
                private modalImageService: ModalImageService) {}  
  
  ngOnInit(): void {
    this.loadRoles();
    this.loadUsers();

    // Con esto indico que se termino de actualizar la imagen desde el popup
    this.imgSubs = this.modalImageService.newImage
    .pipe(
      delay(100) // con esto nos aseguramos que primero se actualice la foto  luego se llama al metodo de cargar usuarios.
    )
    .subscribe( img => {
      this.loadRoles();
      this.loadUsers()
    }); 
  };

  ngOnDestroy(): void {
    this.imgSubs.unsubscribe(); // Con esto evitamos fuga de memoria
  };

  loadUsers() {
    this.loading = true;
    this.userService.loadUsers(this.pagination)
    .subscribe( (resp: any) => {
      this.pagination.pageSize = resp.pagination.pageSize;
      this.pagination.pageNumber = resp.pagination.pageNumber;
      this.pagination.totalItems = resp.pagination.totalCount;
      this.users = resp.data;
      this.usersTemp = resp.data;
      this.loading = false;
    });
  };  

  loadRoles() {
    this.userService.getRolesForManintenance()
    .subscribe((resp: any) => {
      this.roles = resp.roles;
    });
  };

  changePage(pageNumber: number) {
    this.pagination.pageNumber = pageNumber;
    this.loadUsers();
  }

  search(searchTerm: string) {
    if (searchTerm.length == 0 ) {
      this.useSearch = false;
      return this.users = this.usersTemp;
    }

    this.searchesService.search('USERS', searchTerm)
    .subscribe((resp: any) => {
      if (resp.length > 0)
      {
        this.useSearch = true;
        var ids = resp.map((e:any) => e.id);
        // console.log(ids);

        this.userService.findUsersByIdCollection(ids)
        .subscribe((resp: any) => {
          this.users = resp.data;
        });
      }
      else
      {
        this.useSearch = false;
      }
    });
    return [];
  };

  deleteUser(user: User) {
    if (user.id === this.userService.uid) {
      return Swal.fire('Error', 'No puede borrarse a si mismo', 'error');
    }

    if (user.roles?.indexOf('Administrator') !== -1) // Tiene un rol con el valor 'Administrator'
    {
      // Validamos el rol del usuario logueado tiene al menos un rol 'Administrator'
      if ( !(this.userService.roles.indexOf('Administrator') !== -1) ) {
        return Swal.fire('Error', 'Solo un usuario con rol Administrator puede borrar a otro usuario Administrator', 'error');
      } 
      else {
        this.confirmDeleteUser(user);
      }
    }
    else {
      this.confirmDeleteUser(user);
    }   
    return true;
  }

  confirmDeleteUser(user: User) {
    Swal.fire({
      title: "Borrar usuario?",
      text: `Esta a punto de borrar a ${ user.firstName }`,
      icon: "question",
      showCancelButton: true,
      confirmButtonText: "Si, borralo"
    }).then((result) => {
      if (result.isConfirmed) {
        this.userService.deleteUser(user)
        .subscribe({
          next: () => {
            Swal.fire({
              title: "Eliminado!",
              text: "El usuario fue eliminado.",
              icon: "success"
            }).then(() => {
              this.loadUsers();
            });
          },
          error: (err) => {
            Swal.fire('Error', 'Hubo un error durante la eliminacion del usuario', 'error');
          },
          complete: () => {
            console.info('Usuario elmininado correctamente');
          }
        });        
      }      
    });
  };

  onSelectedValues(user: User) {
    // TODO: Validar que no me este actualizando yo mismo
    console.log('userId_logued',this.userService.uid);
    console.log('userId_changed', user.id);
    if (user.roles?.indexOf('Administrator') !== -1)
    {
      Swal.fire({
        title: "Convertir en Administrator?",
        text: `Esta a punto de asignar a ${ user.firstName } el rol Administrator, estas seguro?`,
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Si, procede"
      }).then((result) => {
        if (result.isConfirmed) {
          let rolSelected = user.roles;
          user.roles = [rolSelected?.toString() ?? ''];
          this.userService.updateUser(user)
          .subscribe({
            next: () => {
              Swal.fire({
                title: "Hecho!",
                text: "El rol del usuario fue actualizado.",
                icon: "success"
              }).then(() => {
                this.loadUsers();
              });
            },
            error: (err) => {
              Swal.fire('Error', 'Hubo un error durante la actualizacion del usuario', 'error');
            },
            complete: () => {
              console.info('Usuario actualizado correctamente');
            }
          });        
        }
        else {
          this.loadUsers();
        }
      });      
    }
    else {      
      let rolSelected = user.roles;
      user.roles = [rolSelected?.toString() ?? ''];
      this.userService.updateUser(user)
      .subscribe({
        next: () => {
          this.loadUsers();
        },
        error: (err) => {
          Swal.fire('Error', 'Hubo un error durante la actualizacion del usuario', 'error');
        },
        complete: () => {
          console.info('Usuario actualizado correctamente');
        }
      });
    }
  };

  openModalImage(user: User)
  {
    console.log(user);
    this.modalImageService.openModal('USERS', user.id, user.img)
  }
}
