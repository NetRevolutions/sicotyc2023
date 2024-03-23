import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-lookup-codes',
  templateUrl: './lookup-codes.component.html',
  styles: [
  ]
})
export class LookupCodesComponent implements OnInit{
  public lcgIdSelected: string = '0';

  constructor() {}

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}
