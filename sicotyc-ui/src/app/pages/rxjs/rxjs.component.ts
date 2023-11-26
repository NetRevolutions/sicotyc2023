import { Component, OnDestroy } from '@angular/core';
import { Observable, Subscription, filter, interval, map, retry, take } from 'rxjs';

@Component({
  selector: 'app-rxjs',
  templateUrl: './rxjs.component.html',
  styles: [
  ]
})
export class RxjsComponent implements OnDestroy{

  public intervalSubs: Subscription;

  constructor( ) {   

    // Antigua forma
    // obs$.pipe()
    // .subscribe(
    //   valor => console.log('Subs:', valor ),
    //   error => console.warn('Error', error),
    //   () => console.info('Obs terminado')
    // );

    // Nueva forma
    // this.retornaObservable().pipe(
    //   retry(2) // Intentara 2 veces
    // )
    // .subscribe({ 
    //   next: (valor) => console.log('Subs:', valor),
    //   error: (error) => console.warn('Error', error),
    //   complete: () => console.info('Obs terminado')
    // });

    this.intervalSubs = this.retornaIntervalo()
    .subscribe({
      next: console.log, //(valor) => console.log(valor),
      error: console.warn, //(error) => console.warn('Error', error),
      complete: console.info //() => console.info('Obs terminado')
    });

  };
  
  ngOnDestroy(): void {
    // Cuando salgo de esta pagina se llama a esto.
    // Es util tener un unsubscribe para evitar fuga de memoria.
    this.intervalSubs.unsubscribe();
  };


  retornaIntervalo(): Observable<number> {
    return interval(1000)
              .pipe(
                //take(10),
                map( valor => valor + 1),
                filter( valor => ( valor % 2 === 0 ) ? true : false )
              );    
  }

  retornaObservable(): Observable<number> {
    let i = -1;

    return new Observable<number>( observer => {
      const intervalo = setInterval( () => {
        i++;
        observer.next(i); // Con observer.next([valor a emitir]) es cuando verdaderamente emito los valores 
        
        if ( i === 4 ) {
          clearInterval( intervalo ); // Con esto cancelamos el intervalo.
          observer.complete(); // Con esto indica que ya termino el observable
        }

        // if ( i === 2 ) {          
        //   observer.error('i llego al valor de 2');
        // }       

      }, 1000);

    });
    
  };

}


