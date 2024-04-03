import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, throwError } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Router } from '@angular/router';

// Enums
import { EnumLookupCodeGroups } from '../enum/enums.enum';

// Interfaces
import { ILookupCode, ILookupCodeGroup, IRegisterLookupCode, IRegisterLookupCodeGroup, IUpdateLookupCode, IUpdateLookupCodeGroup } from '../interfaces/lookup.interface';
import { IPagination } from '../interfaces/pagination.interface';

// Services
import { UserService } from './user.service';
import { ValidationErrorsCustomizeService } from './validation-errors-customize.service';

const base_url = environment.base_url;

@Injectable({
  providedIn: 'root'
})
export class LookupService {

  constructor(private http: HttpClient,
              private userService: UserService,
              private router: Router,
              private validationErrorsCustomize: ValidationErrorsCustomizeService) { }

  get token(): string {

    return localStorage.getItem('token') || '';
  };

  get headers() {
    return {
      headers: {
        'x-token': this.token
      }
    }
  };

  //#region LookupCodeGroup
  
  createLookupCodeGroup(formData: IRegisterLookupCodeGroup) {
    var claims = this.userService.getClaimsFromLocalStorage(); 
    var userId = claims.find(f => f.type == 'Id').value.toString();
    
    const data = {
      ...formData, 
      createdBy: userId 
    };
    return this.http.post(`${ base_url }/lookupCodeGroups`, data, this.headers)
    .pipe(
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })       
    );
  };

  updateLookupCodeGroup(formData: IUpdateLookupCodeGroup) {  
    var claims = this.userService.getClaimsFromLocalStorage(); 
    var userId = claims.find(f => f.type == 'Id').value.toString();  
    let arrLC: any[] = [];
        
    if (formData.lookupCodes !== null)
    {
      formData.lookupCodes?.forEach(i => {
        arrLC.push(
          { lookupCodeId: i.lookupCodeId }, 
          { lookupCodeGroupId: i.lookupCodeGroupId },
          { lookupCodeName: i.lookupCodeName },
          { lookupCodeValue: i.lookupCodeValue },
          { lookupCodeOrder: i.lookupCodeOrder },
          { createdBy: userId} 
          )    
      });
    };

    const data = {
      ...formData,
      updatedBy: userId
    };

    data.lookupCodes = arrLC;

    return this.http.put(`${ base_url }/lookupCodeGroups/${formData.lookupCodeGroupId}`, data, this.headers)
    .pipe(
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    );
  };

  getLookupCodeGroups(pagination: IPagination, searchTerm: string = '') {
    const url = `${ base_url }/lookupCodeGroups/getLookupCodeGroups?pageNumber=${pagination.pageNumber}&pageSize=${pagination.pageSize}&searchTerm=${searchTerm}`;
    return this.http.get(url) //, this.headers
      .pipe(
        map((resp: any) => {
          let lookupCodeGroups: ILookupCodeGroup[] = resp.data.map((lcg: any) => ({ 
            lookupCodeGroupId: lcg.id, 
            lookupCodeGroupName: lcg.name
          }));
          
          return {
            data: lookupCodeGroups,
            pagination: resp.pagination
          }
        }),
        catchError(error => {
          return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
        })   
      );
  };

  getAllLookupCodeGroups() {
    const url = `${ base_url }/lookupCodeGroups/getLookupCodeGroups/All`;
    return this.http.get(url) //, this.headers
      .pipe(
        map((resp: any) => {
          let lookupCodeGroups: ILookupCodeGroup[] = resp.map((lcg: any) => ({ 
            lookupCodeGroupId: lcg.id, 
            lookupCodeGroupName: lcg.name
          }));
          
          return {
            data: lookupCodeGroups
          }
        }),
        catchError(error => {
          return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
        })   
      );
  };

  getLookupCodeGroupById(id: string) {
    const url = `${ base_url }/lookupCodeGroups/lookupCodeGroupById/${id}`;
    return this.http.get(url) //, this.headers
      .pipe(
        map((resp: any) => {
          let lookupCodeGroup: ILookupCodeGroup = resp.data;

          return {
            data: lookupCodeGroup
          }
        }),
        catchError(error => {
          return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
        })   
      );
  };

  existsLookupCodeGroup(lcgName: string) {
    const url = `${ base_url }/lookupCodeGroups/existsLookupCodeGroup/${lcgName}`;
    return this.http.get<boolean>(url)
    .pipe(
      map((resp: boolean) => resp),
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    );
  };

  findLookupCodeGroupsByIdCollection(ids: string[]) {
    const url = `${ base_url }/lookupCodeGroups/collection(${ids.join(',')})`;
    return this.http.get(url)
    .pipe(
      map((resp: any) => {
        let lookupCodeGroups: ILookupCodeGroup[] = resp.map((lcg: any) => ({ 
          lookupCodeGroupId: lcg.id, 
          lookupCodeGroupName: lcg.name
        }));
        return {
          data: lookupCodeGroups
        }
      }),
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    )
  };

  deleteLookupCodeGroup(id?: string) {
    const url = `${ base_url }/lookupCodeGroups/${id}`;
    return this.http.delete(url, this.headers);
  };

  //#endregion

  //#region Lookup Code
  createLookupCode(formData: IRegisterLookupCode) {
    var claims = this.userService.getClaimsFromLocalStorage(); 
    var userId = claims.find(f => f.type == 'Id').value.toString();

    const data = {
      ...formData,
      createdBy: userId
    };
    return this.http.post(`${ base_url }/lookupCodeGroups/${formData.lookupCodeGroupId}/lookupCodes`, data, this.headers)
    .pipe(
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    );
  };

  updateLookupCode(formData: IUpdateLookupCode) {
    var claims = this.userService.getClaimsFromLocalStorage(); 
    var userId = claims.find(f => f.type == 'Id').value.toString();
    const data = {
      ...formData,
      updatedBy: userId
    };
    return this.http.put(`${ base_url }/lookupCodeGroups/${formData.lookupCodeGroupId}/lookupCodes/${formData.lookupCodeId}`, data, this.headers)
    .pipe(
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    );
  } ;

  getLookupCodesByLCGId(lcgId: string, pagination: IPagination, searchTerm: string = '') {
    const url = `${ base_url }/lookupCodeGroups/${lcgId}/lookupCodes?pageNumber=${pagination.pageNumber}&pageSize=${pagination.pageSize}&searchTerm=${searchTerm}`;
    return this.http.get(url) //, this.headers
    .pipe(
      map((resp: any) => {
        let lookupCodes: ILookupCode[] = resp.data.map((lc: any) => ({ 
          lookupCodeId: lc.id,
          lookupCodeGroupId: lc.lookupCodeGroupId, 
          lookupCodeName: lc.lookupCodeName,
          lookupCodeValue: lc.lookupCodeValue,
          lookupCodeOrder: lc.lookupCodeOrder
        }));
        return {
          data: lookupCodes,
          pagination: resp.pagination
        }
      }),
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    )
  };

  getLookupCodesByLCGName(lcgName: string, pagination: IPagination, searchTerm: string = '') {    
    this.getLookupCodeGroups(pagination, '')
    .subscribe((resp: any) => {
      let lstLCG: ILookupCodeGroup[] = resp.data;
      let lcgId = lstLCG.find(lcg => lcg.lookupCodeGroupName == lcgName)?.lookupCodeGroupId;

      if ( !lcgId )
      return;

      return this.getLookupCodesByLCGId(lcgId, pagination, searchTerm);

    })
  };
  

  getLookupCodesByLCGNameALL(lcgName: EnumLookupCodeGroups) {
    let lcgId: string = '00000000-0000-0000-0000-000000000000';
                      
    const url = `${ base_url }/lookupCodeGroups/${lcgId}/lookupCodes/LookupCodesByGroupName/${lcgName}`;
    return this.http.get(url)
    .pipe(
      map((resp: any) => {
        let lookupCodes: ILookupCode[] = resp.data.map((lc: any) => ({ 
          lookupCodeId: lc.id,
          lookupCodeGroupId: lc.lookupCodeGroupId, 
          lookupCodeName: lc.lookupCodeName,
          lookupCodeValue: lc.lookupCodeValue,
          lookupCodeOrder: lc.lookupCodeOrder
        }));
        return {
          data: lookupCodes
        }
      }),
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    )
  };

  getLookupCode(lcgId: string, lcId: string) {
    const url = `${ base_url }/lookupCodeGroups/${lcgId}/lookupCodes/${lcId}`;
    return this.http.get(url, this.headers)
    .pipe(
      map((resp: any) => {        
        let lookupCode: ILookupCode = {
          lookupCodeId : resp.id,
          lookupCodeGroupId: resp.lookupCodeGroupId,
          lookupCodeName: resp.lookupCodeName,
          lookupCodeValue: resp.lookupCodeValue,
          lookupCodeOrder: resp.lookupCodeOrder
        }

        return {
          data: lookupCode
        }
      }),
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    )
  };

  findLookupCodesByIdCollection(lcgId: string, ids: string[]) {    
    const url = `${ base_url }/lookupCodeGroups/${lcgId}/lookupCodes/collection(${ids.join(',')})`;
    return this.http.get(url)
    .pipe(
      map((resp: any) => {
        let lookupCodes: ILookupCode[] = resp.map((lc: any) => ({ 
          lookupCodeId: lc.id,
          lookupCodeGroupId: lc.lookupCodeGroupId, 
          lookupCodeName: lc.lookupCodeName,
          lookupCodeValue: lc.lookupCodeValue,
          lookupCodeOrder: lc.lookupCodeOrder
        }));

        return {
          data: lookupCodes
        }
      }),
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    )
  };

  deleteLookupCode(lcgId: string, id?: string) {
    const url = `${ base_url }/lookupCodeGroups/${lcgId}/lookupCodes/${id}`;
    return this.http.delete(url, this.headers)
    .pipe(
      catchError(error => {
        return throwError(() => new Error(this.validationErrorsCustomize.messageCatchError(error)));
      })   
    );
  };
  //#endregion

  //#region Specific LookupCodes
  getTypeOfDocuments() {
    return this.getLookupCodesByLCGNameALL(EnumLookupCodeGroups.TIPO_DOCUMENTO);    
  };

  getTypeOfCompanies() {
    return this.getLookupCodesByLCGNameALL(EnumLookupCodeGroups.TIPO_DE_EMPRESA);
  };

  getTypeOfTolls() {
    return this.getLookupCodesByLCGNameALL(EnumLookupCodeGroups.TIPO_PAGO_PEAJE);
  };

  getTypeOfServices() {
    return this.getLookupCodesByLCGNameALL(EnumLookupCodeGroups.TIPO_DE_SERVICIO);
  };


  //#region 
}
