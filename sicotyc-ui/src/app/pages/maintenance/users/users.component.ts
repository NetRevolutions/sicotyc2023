import { Component, OnDestroy, OnInit } from '@angular/core';
import Swal from 'sweetalert2';

// Model
import { User } from 'src/app/models/user.model';

// Services
import { SearchesService } from 'src/app/services/searches.service';
import { UserService } from 'src/app/services/user.service';
import { ModalImageService } from 'src/app/services/modal-image.service';
import { Subscription, delay } from 'rxjs';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styles: [
  ]
})
export class UsersComponent implements OnInit, OnDestroy{
  public totalUsers: number = 0;
  public users: User[] = [];
  public usersTemp: User[] = [];
  public from: number = 0;
  public loading: boolean = true;
  public useSearch: boolean = false;
  public imgSubs: Subscription = new Subscription();

  constructor( private userService: UserService,
                private searchesService: SearchesService,
                private modalImageService: ModalImageService) {}  
  
  ngOnInit(): void {
    this.loadUsers();

    // Con esto indico que se termino de actualizar la imagen desde el popup
    this.imgSubs = this.modalImageService.newImage
    .pipe(
      delay(100) // con esto nos aseguramos que primero se actualice la foto  luego se llama al metodo de cargar usuarios.
    )
    .subscribe( img => {
      this.loadUsers()
    }); 
  };

  ngOnDestroy(): void {
    this.imgSubs.unsubscribe(); // Con esto evitamos fuga de memoria
  };

  loadUsers() {
    this.loading = true;
    this.userService.loadUsers(this.from)
    .subscribe( (resp: any) => {
      this.totalUsers = resp.pagination.totalCount;
      this.users = resp.data;
      this.usersTemp = resp.data;
      this.loading = false;      
      //console.log(this.users);
    });
  }

  changePage(value: number) {
    this.from += value;

    if (this.from < 0) {
      this.from = 0
    }
    else if (this.from >= this.totalUsers ) {
      this.from -= value;
    }

    this.loadUsers();
  };

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
          next: (resp) => {
            //console.log(resp);
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
          this.userService.updateProfile(user)
          .subscribe({
            next: (resp) => {
              //console.log(resp);
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
      this.userService.updateProfile(user)
      .subscribe({
        next: (resp) => {
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
