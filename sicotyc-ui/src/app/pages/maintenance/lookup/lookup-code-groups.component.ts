import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';

import { ILookupCodeGroup, IUpdateLookupCodeGroup } from 'src/app/interfaces/lookup.interface';
import { IPagination } from 'src/app/interfaces/pagination.interface';

import { LookupService } from 'src/app/services/lookup.service';
import { SearchesService } from 'src/app/services/searches.service';

@Component({
  selector: 'app-lookup-code-groups',
  templateUrl: './lookup-code-groups.component.html',
  styles: [
  ]
})
export class LookupCodeGroupsComponent implements OnInit {
  public loading: boolean = true;
  public pagination: IPagination = {
    pageNumber: 0,
    pageSize: 5,
    totalItems: 0
  };
  public useSearch: boolean = false;

  public lookupCodeGroups: ILookupCodeGroup[] = [];
  public lookupCodeGroupsTemp: ILookupCodeGroup[] = [];

  constructor(
    private lookupService: LookupService,
    private searchService: SearchesService
  ) {}


  ngOnInit(): void {
    this.loadLookupCodeGroups();
  }

  loadLookupCodeGroups() {
    this.loading = true;
    this.lookupService.getLookupCodeGroups(this.pagination)
    .subscribe((resp: any) => {
      this.pagination.pageSize = resp.pagination.pageSize;
      this.pagination.pageNumber = resp.pagination.pageNumber;
      this.pagination.totalItems = resp.pagination.totalCount;
      this.lookupCodeGroups = resp.data;
      this.lookupCodeGroupsTemp = resp.data;
      this.loading = false;
    });
  }

  search(searchTerm: string) {
    if ( searchTerm.length == 0 ) {
      this.useSearch = false;
      return this.lookupCodeGroups = this.lookupCodeGroupsTemp;
    }

    this.searchService.search('LOOKUPCODEGROUPS', searchTerm)
    .subscribe((resp: any) => {
      if (resp.length > 0) {
        this.useSearch = true;
        var ids = resp.map((e:any) => e.id)

        this.lookupService.findLookupCodeGroupsByIdCollection(ids)
        .subscribe((resp: any) => {
          this.lookupCodeGroups = resp.data;
        });
      }
      else 
      {
        this.useSearch = false;
      }
    });
    return [];
  }  

  changePage(pageNumber: number) {
    this.pagination.pageNumber = pageNumber;
    this.loadLookupCodeGroups();
  }

  async popupSweetAlertNew() {
    const { value = '' } = await Swal.fire<string>({
      title: 'Crear Lookup Code Group',
      text: 'Ingrese el nombre del Lookup Code Group',
      input: 'text',
      inputPlaceholder: 'Nombre del Lookup Code Group',
      showCancelButton: true
    });

    if (value.trim().length > 0 ) {
      this.lookupService.existsLookupCodeGroup(value.trim())
      .subscribe((existLCG: boolean) =>{
        if (!existLCG) {
          let data: ILookupCodeGroup = {
            lookupCodeGroupName: value
          };          
          this.lookupService.createLookupCodeGroup( data )
          .subscribe( (resp: any ) => {
            //this.lookupCodeGroups.push(resp.data);
            this.loadLookupCodeGroups();
          });
        }
        else {
          Swal.fire('Error', `El Lookup Code Group ${ value } ya existe, intente otro nombre`, 'error');
        }
      });      
    };
  };

  async popupSweetAlertEdit(entity: ILookupCodeGroup) {    
    const { value = '' } = await Swal.fire<string>({
      title: "Editar Lookup Code Group",
      text: 'Ingrese el nombre del Lookup Code Group',
      input: 'text',
      inputValue: entity.lookupCodeGroupName,
      inputPlaceholder: 'Nombre del Lookup Code Group',
      showCancelButton: true
    });

    if (value.trim().length > 0) {
      this.lookupService.existsLookupCodeGroup(value.trim())
      .subscribe((existsLCG: boolean) => {
        if (!existsLCG){
          let data: IUpdateLookupCodeGroup = {
            lookupCodeGroupId: entity.lookupCodeGroupId,
            lookupCodeGroupName: value
          };
          this.lookupService.updateLookupCodeGroup(data)
          .subscribe((resp: any) => {
            this.loadLookupCodeGroups();
            Swal.fire('Actualizado', 'Lookup Code Group actualizado correctamente', 'success');
          });
        }
        else {
          Swal.fire('Error', `El Lookup Code Group ${ value } ya existe, solo se puede cambiar de nombre y/o eliminar`, 'error');
        }
      });
    }
  };

  delete(entity: ILookupCodeGroup) {
    return this.confirmDelete(entity);
  };

  confirmDelete(entity: ILookupCodeGroup) {
    Swal.fire({
      title: 'Borrar Lookup Code Group?',
      text: `Esta a punto de borrar el Lookup Code Group: ${ entity.lookupCodeGroupName }, sus lookup codes tambien seran borrados`,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Si, borralo'
    }).then((result) => {
      if (result.isConfirmed) {
        this.lookupService.deleteLookupCodeGroup(entity.lookupCodeGroupId)
        .subscribe({
          next: (resp) => {
            Swal.fire({
              title: 'Eliminado!',
              text: 'El Lookup Code Group con sus Lookup Codes fueron eliminados',
              icon: "success"
            }).then(() => {
              this.loadLookupCodeGroups();
            });
          },
          error: (err) => {
            Swal.fire('Error', 'Hubo un error durante el proceso de eliminacion, verifique!!!', 'error');
          },
          complete: () => {
            console.info('Lookup Code Groups y Lookup Codes fueron eliminados correctamente');
          }
        });
      }
    })
  };

}
