import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ILookupCode } from 'src/app/interfaces/lookup.interface';

import { LookupService } from 'src/app/services/lookup.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lookup-code',
  templateUrl: './lookup-code.component.html',
  styles: [
  ]
})
export class LookupCodeComponent implements OnInit{
  public lookupCodeForm: FormGroup;
  public lookupCodeGroupId: string = '0';
  public lookupCodeSelected?: ILookupCode;
  public title: string = 'Lookup Code';
  public subtitle: string = 'Actualizar Información';

  constructor(
    private fb: FormBuilder,
    private lookupService: LookupService,
    private router: Router,
    private activatedRouter: ActivatedRoute
  ) {
    this.lookupCodeForm = this.fb.group({});
  };

  ngOnInit(): void {  
    this.activatedRouter.params
    .subscribe(params => {
      this.lookupCodeGroupId = params['lcgId'];
      this.loadLookupCode(params['lcgId'], params['lcId']);
    });

    this.lookupCodeForm = this.fb.group({
      lookupCodeValue: ['', Validators.required ],
      lookupCodeName: ['', Validators.required ],
      lookupCodeOrder: ['', Validators.required ]      
    });
  };

  loadLookupCode(lcgId: string, lcId: string) {
    if (lcId === 'new'){
      this.subtitle = 'Crear nueva información';
      return;
    }
      

    this.lookupService.getLookupCode( lcgId, lcId )
    .subscribe((resp: any) => {
      const { lookupCodeValue, lookupCodeName, lookupCodeOrder } = resp.data;
      this.lookupCodeSelected = resp.data;
      this.lookupCodeForm.setValue({lookupCodeValue, lookupCodeName, lookupCodeOrder});
      return false;
    })
  };

  save() {  
    // TODO: Validar si dentro de los LC asociados a este LCG ya existe alguno con el mismo nombre y/o valor  
    if (this.lookupCodeSelected) {
      // actualizar
      const data = {
        ...this.lookupCodeForm.value,
        lookupCodeGroupId : this.lookupCodeSelected.lookupCodeGroupId,
        lookupCodeId : this.lookupCodeSelected.lookupCodeId
      };
      this.lookupService.updateLookupCode(data)
      .subscribe((resp: any) => {
        Swal.fire('Actualizado', 'Lookup Code actualizado correctamente', 'success');
      });
    }
    else {
      // crear
      const data = {
        ...this.lookupCodeForm.value,
        lookupCodeGroupId : this.lookupCodeGroupId
      };
      this.lookupService.createLookupCode(data)
      .subscribe((resp: any) => {
        Swal.fire('Creado', 'Lookup Code creado correctamente', 'success');
        this.router.navigateByUrl(`/mantenimientos/lookupCodeGroups/${resp.lookupCodeGroupId}/lookupCodes/${resp.id}`);
      });
    }  
  };
}
