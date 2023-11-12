import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-incrementador',
  templateUrl: './incrementador.component.html',
  styles: [
  ]
})
export class IncrementadorComponent implements OnInit{
  
  // @Input('valor') progreso: number = 40;
  @Input('valor') progreso: number = 40;
  @Input() btnClass: string = 'btn-primary';
  
  
  @Output() valorSalida: EventEmitter<number> = new EventEmitter();
  
  ngOnInit() {
    this.btnClass = `btn ${ this.btnClass }`;
  }
  
  cambiarValor( valor: number): void {

    if ( this.progreso >= 100 && valor >= 0 ) {
      this.valorSalida.emit(100);
      this.progreso = 100;      
    };

    if ( this.progreso <= 0 && valor < 0 ) {
      this.valorSalida.emit(0);
      this.progreso = 0;
    }

    this.progreso = this.progreso + valor;
    this.valorSalida.emit(this.progreso);
  }

  onChange( nuevoValor: number ) {
    if (nuevoValor >= 100) {
      this.progreso = 100;
    } else if (nuevoValor <= 0) {
      this.progreso = 0;
    } else {
      this.progreso = nuevoValor;
    }
    this.valorSalida.emit( this.progreso );
  }
}
