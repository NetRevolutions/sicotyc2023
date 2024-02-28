import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';

// Model
import { User } from 'src/app/models/user.model';

// Services
import { SearchesService } from 'src/app/services/searches.service';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styles: [
  ]
})
export class UsersComponent implements OnInit{
  public totalUsers: number = 0;
  public users: User[] = [];
  public usersTemp: User[] = [];
  public from: number = 0;
  public loading: boolean = true;
  public useSearch: boolean = false;

  constructor( private userService: UserService,
                private searchesService: SearchesService) {}
  
  ngOnInit(): void {
    this.loadUsers();
  };


  loadUsers() {
    this.loading = true;
    this.userService.loadUsers(this.from)
    .subscribe( (resp: any) => {
      this.totalUsers = resp.pagination.totalCount;
      this.users = resp.data;
      this.usersTemp = resp.data;
      this.loading = false;

      // console.log(resp);
      // console.log('totalUsers',this.totalUsers);
      // console.log('users', this.users);
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
        // TODO: llamar a un metodo que trae todos los usuarios basado en la coleccion de Ids. 
        // this.users = this.users.filter(obj => {
        //   return resp.some((item: any) => item.id == obj.id)
        // });

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
    console.log(user);
  }
}
