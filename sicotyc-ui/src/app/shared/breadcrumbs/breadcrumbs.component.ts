import { Component, OnDestroy } from '@angular/core';
import { ActivationEnd, Router } from '@angular/router';
import { Subscription, filter, map } from 'rxjs';

@Component({
  selector: 'app-breadcrumbs',
  templateUrl: './breadcrumbs.component.html',
  styles: [
  ]
})
export class BreadcrumbsComponent implements OnDestroy{

  public title: string = '';
  public titleSubs$: Subscription;
  
  constructor( private router: Router) { 
    
    this.titleSubs$ = this.getDataRuta()
    .subscribe(({ title }) => {      
      this.title = title;
      // Poner el nombre de la pagina en el titulo de la pagina web
      document.title = `Sicotyc - ${ title }`;
    });
  }
  
  ngOnDestroy(): void {
    this.titleSubs$.unsubscribe();
  }
;

  getDataRuta() {
    return this.router.events
    .pipe(
      filter((event): event is ActivationEnd => event instanceof ActivationEnd),
      filter((event: ActivationEnd) => event.snapshot.firstChild === null),
      map((event:ActivationEnd) => event.snapshot.data)
    );
    
  }

  

}
