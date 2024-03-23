import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

import { ILookupCode, ILookupCodeGroup, IRegisterLookupCode, IRegisterLookupCodeGroup, IUpdateLookupCode, IUpdateLookupCodeGroup } from '../interfaces/lookup.interface';

import { UserService } from './user.service';
import { map } from 'rxjs';

const base_url = environment.base_url;

@Injectable({
  providedIn: 'root'
})
export class LookupService {

  constructor(private http: HttpClient,
              private userService: UserService) { }

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
    return this.http.post(`${ base_url }/lookupCodeGroups`, data, this.headers);
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

    return this.http.put(`${ base_url }/lookupCodeGroups/${formData.lookupCodeGroupId}`, data, this.headers);
  };

  getLookupCodeGroups(from: number = 0, pageSize: number = 5, searchTerm: string = '') {
    const url = `${ base_url }/lookupCodeGroups/getLookupCodeGroups?pageNumber=${from}&pageSize=${pageSize}&searchTerm=${searchTerm}`;
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
    return this.http.post(`${ base_url }/lookupCodeGroups/${formData.lookupCodeGroupId}/lookupCodes`, data, this.headers);
  }

  updateLookupCode(formData: IUpdateLookupCode) {
    var claims = this.userService.getClaimsFromLocalStorage(); 
    var userId = claims.find(f => f.type == 'Id').value.toString();
    const data = {
      ...formData,
      updatedBy: userId
    };
    return this.http.put(`${ base_url }/lookupCodeGroups/${formData.lookupCodeGroupId}/lookupCodes/${formData.lookupCodeId}`, data, this.headers);
  }  

  getLookupCodesByLCGId(lcgId: string, from: number = 0, pageSize: number = 5, searchTerm: string = '') {
    const url = `${ base_url }/lookupCodeGroups/${lcgId}/lookupCodes?pageNumber=${from}&pageSize=${pageSize}&searchTerm=${searchTerm}`;
    return this.http.get(url) //, this.headers
    .pipe(
      map((resp: any) => {
        let lookupCodes: ILookupCode[] = resp.data;
        return {
          data: lookupCodes,
          pagination: resp.pagination
        }
      })
    )
  };

  getLookupCodesByLCGName(lcgName: string, from: number = 0, pageSize: number = 5, searchTerm: string = '') {
    this.getLookupCodeGroups(0, 50, '')
    .subscribe((resp: any) => {
      let lstLCG: ILookupCodeGroup[] = resp.data;
      let lcgId = lstLCG.find(lcg => lcg.lookupCodeGroupName == lcgName)?.lookupCodeGroupId;

      if ( !lcgId )
      return;

      return this.getLookupCodesByLCGId(lcgId, from, pageSize, searchTerm);

    })
  }

  getLookupCode(lcgId: string, lcId: string) {
    const url = `${ base_url }/lookupCodeGroups/${lcgId}/lookupCodes/GetLookupCodeForLookupCodeGroup/${lcId}`;
    return this.http.get(url, this.headers)
    .pipe(
      map((resp: any) => {
        let lookupCode: ILookupCode = resp.data;

        return {
          data: lookupCode
        }
      })
    )
  }

  deleteLookupCode(lcgId: string, id: string) {
    const url = `${ base_url }/lookupCodeGroups/${lcgId}/lookupCodes/${id}`;
    return this.http.delete(url, this.headers);
  }
  //#endregion

}
