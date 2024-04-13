import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

// Interfaces
import { ILookupCode, ILookupCodeGroup } from 'src/app/interfaces/lookup.interface';

// Models
import { User } from 'src/app/models/user.model';

// Services
import { SearchesService } from 'src/app/services/searches.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styles: [
  ]
})
export class SearchComponent implements OnInit {

  public users: User[] = [];
  public lookupCodeGroups: ILookupCodeGroup[] = [];
  public lookupCodes: ILookupCode[] = [];
  public transports: any[] = []; // Pending implement interface

  constructor(
    private activatedRoute: ActivatedRoute,
    private searchService: SearchesService
  ) {};
  

  ngOnInit(): void {
    this.activatedRoute.params
    .subscribe(({ term }) => this.globalSearch( term ));
  };


  globalSearch(term: string) {
    this.searchService.searchAll(term)
    .subscribe((resp: any) => {
      console.log(resp);
      // this.users = resp.users;
      // this.lookupCodeGroups = resp.lookupCodeGroups;
      // this.lookupCodes = resp.lookupCodes;
      // this.transports = resp.transports;
    })
  };

  openUser(user: User) {

  };

  openLookupCode(lookupCode: ILookupCode) {

  };

  openLookupCodeGroup(lookupCodeGroup: ILookupCodeGroup) {

  };

  openTransport(transport: any) {

  };
}
