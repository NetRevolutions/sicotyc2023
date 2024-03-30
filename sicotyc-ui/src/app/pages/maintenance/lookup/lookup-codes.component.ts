import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ILookupCode, ILookupCodeGroup } from 'src/app/interfaces/lookup.interface';
import { IPagination } from 'src/app/interfaces/pagination.interface';

import { LookupService } from 'src/app/services/lookup.service';
import { SearchesService } from 'src/app/services/searches.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lookup-codes',
  templateUrl: './lookup-codes.component.html',
  styles: [
  ]
})
export class LookupCodesComponent implements OnInit{
  public lcgIdSelected: string = '';

  public loading: boolean = true;
  public pagination: IPagination = {
    pageNumber: 1,
    pageSize: 5,
    totalItems: 0
  };

  public lookupCodeGroups: ILookupCodeGroup[] = [];
  public lookupCodes: ILookupCode[] = [];
  public lookupCodesTemp: ILookupCode[] = [];

  constructor(
    private lookupService: LookupService,
    private searchService: SearchesService,
    private router: Router
  ){};

  ngOnInit(): void {
    this.loadAllLookupCodeGroups();
  };

  loadAllLookupCodeGroups() {
    this.loading = true;
    this.lookupService.getAllLookupCodeGroups()
    .subscribe((resp: any) => {
      this.lookupCodeGroups = resp.data;
      this.loading = false;
    })
  };

  onChangeLookupCodeGroup(event: any) {
    this.lcgIdSelected = event.target.value;
    this.pagination.pageNumber = 1;
    this.pagination.pageSize = 5;
    this.pagination.totalItems = 0;

    this.loadLookupCodes();
  };

  loadLookupCodes() {
    if (this.lcgIdSelected == '')
    {
      this.lookupCodes = [];
      this.lookupCodesTemp = [];
    }
    else {
      this.loading = true;
      this.lookupService.getLookupCodesByLCGId(this.lcgIdSelected, this.pagination)
      .subscribe((resp: any) =>{        
        this.pagination.pageSize = resp.pagination.pageSize;
        this.pagination.pageNumber = resp.pagination.pageNumber;
        this.pagination.totalItems = resp.pagination.totalCount;
        this.lookupCodes = resp.data;
        this.lookupCodesTemp = resp.data;
        this.loading = false;
      });
    }
  };

  search(searchTerm: string) {
    if ( searchTerm.length == 0 ) {
      return this.lookupCodes = this.lookupCodesTemp;
    }

    let searchKeyTerm = this.lcgIdSelected.toString()+'|'+searchTerm;
    this.searchService.search('LOOKUPCODES', searchKeyTerm)
    .subscribe({
      next: (resp: any) => {
        if (resp.length > 0) {
          var ids = resp.map((e:any) => e.id)
  
          this.lookupService.findLookupCodesByIdCollection(this.lcgIdSelected, ids)
          .subscribe((resp: any) => {          
            this.lookupCodes = resp.data;
          });
        }
      },
      error: (err) =>{
        console.error(err);        
      },
      complete: () => { }
    });    
    return [];
  };

  // changePage(value: number) {
  //   this.from += value;

  //   if(this.from < 0) {
  //     this.from = 0;
  //   }
  //   else if (this.from >= this.totalItems) {
  //     this.from -= value;
  //   } else if (this.from < this.totalItems) {
  //     this.from += value;
  //   }

  //   this.loadLookupCodes();
  // };

  changePage(pageNumber: number){
    this.pagination.pageNumber = pageNumber;
    this.loadLookupCodes();
  }

  delete(entity: ILookupCode) {
    return this.confirmDelete(entity);
  };  

  confirmDelete(entity: ILookupCode) {
    // TODO: Validar si esta siendo usado
    Swal.fire({
      title: 'Borrar Lookup Code?',
      text: `Esta a punto de borrar el Lookup Code ${entity.lookupCodeName}, esta seguro?`,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Si, borralo'
    }).then((result) => {
      if (result.isConfirmed) {
        this.lookupService.deleteLookupCode(entity.lookupCodeGroupId, entity.lookupCodeId)
        .subscribe({
          next: (resp) => {
            Swal.fire({
              title: 'Eliminado!',
              text: 'El Lookup Code fue eliminado',
              icon: "success"
            }).then(() => {
              this.loadLookupCodes();
            });
          },
          error: (err) => {
            Swal.fire('Error', 'Hubo un error durante el proceso de eliminacion, verifique!!!', 'error');
          },
          complete: () => {
            console.info('Lookup Code fue eliminado correctamente');
          }
        })
      }
    });
  };
}
