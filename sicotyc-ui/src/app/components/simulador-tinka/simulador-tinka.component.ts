import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-simulador-tinka',
  templateUrl: './simulador-tinka.component.html',
  styleUrls: [ './simulador-tinka.component.css' ]
})
export class SimuladorTinkaComponent implements OnInit{
  @Input() titulo: string = 'Simulador Tinka';
  @Input() numeroMinimo: number = 1;
  @Input() numeroMaximo: number = 48;
  @Input() numeroColumnas: number = 6;
  @Input() numeroAciertos: number = 6;
  resultados: Array<number> = [];
  
  @Output() valorSalida: EventEmitter<Array<number>> = new EventEmitter();
  
  ngOnInit() {
   // Obtener el contenedor de la tabla
    const tablaContainer = document.getElementById('tabla-container');    

    // Verificar que el contenedor exista
    if (tablaContainer) {
        // Crear tabla con 3 filas y 4 columnas
        const tabla = this.crearTabla();

        // Agregar la tabla al contenedor
        tablaContainer.appendChild(tabla);
    }
    
  }
  onChange() {

    this.valorSalida.emit( this.resultados );
  }

  crearTabla(): HTMLTableElement {

    // Obtener Filas
    let filas = Math.trunc(this.numeroMaximo / this.numeroColumnas);
    let residuo = this.numeroMaximo % this.numeroColumnas;
    filas = residuo > 0 ? filas + 1 : filas;    
    let columnas = this.numeroColumnas;


    // Crear tabla
    const tabla = document.createElement('table');
    
    // Crear filas y celdas
    for (let i = 0; i < filas; i++) {
      const fila = tabla.insertRow();

      for (let j = 0; j < columnas; j++) {
          const celda = fila.insertCell();
          const numeroCelda = (i * columnas) + j + 1;

          // Agregar un id a cada celda
          celda.id = `celda-${numeroCelda}`;

          // Mostrar el número de la celda
          celda.textContent = numeroCelda.toString();

          // Agregando Style a la celda
          celda.style.border = "1px solid #dddddd";
          celda.style.padding = "8px";
          celda.style.width = "40px";

          // Agregar un evento de clic a cada celda para resaltarla
          celda.addEventListener('click', () => this.resaltarCelda(celda.id));
          
          // Validamos si la celda no excede el maximo permitido
          if (numeroCelda == this.numeroMaximo) {
            break;
          }
      }
    }

    return tabla;

  };

  // Función para resaltar una celda específica
  resaltarCelda(idCelda: string, isAzar: boolean = false) {
    const celdaResaltada = document.getElementById(idCelda);   

    // Verificar que la celda exista
    if (celdaResaltada) {

        if (celdaResaltada.textContent)
        {
          if (!isAzar) {
            if (this.resultados.length >= this.numeroAciertos) return;  
            this.resultados.push(parseInt(celdaResaltada.textContent));
          }
          // Aplicar algún estilo de resaltado (por ejemplo, un fondo de color)
          celdaResaltada.style.backgroundColor = 'yellow';
          this.valorSalida.emit(this.resultados);
        }
    }
  };
  
  limpiarValores() {
    if (this.resultados.length > 0) {
      this.resultados.forEach(element => {        
        const celdaResaltada = document.getElementById(`celda-${element}`);

        // Verificar que la celda exista
        if (celdaResaltada) {
          // Remover estilo de resaltado (por ejemplo, un fondo de color)      
          celdaResaltada.style.removeProperty('background-color');
        }
      });
      this.resultados = [];
      this.valorSalida.emit(this.resultados);
    }    
  };

  // Aca llamamos tantas veces como fue definido el nro de aciertos y verificamos que no se repitan
  AlAzar() {
    this.limpiarValores();
    while(this.resultados.length != this.numeroAciertos) {

    let valorRandom = this.getRandomIntInclusive();
    if (!this.resultados.includes(valorRandom)) {
      this.resultados.push(valorRandom);
    }
   }
   
   if (this.resultados.length > 0) {
    this.resultados.forEach(element => {
      this.resaltarCelda(`celda-${element}`, true);
    });
    this.valorSalida.emit(this.resultados);
   }
  }

  getRandomIntInclusive(): number {
    // The maximum is inclusive and the minimum is inclusive
    let min = Math.ceil(this.numeroMinimo);
    let max = Math.floor(this.numeroMaximo);

    return Math.floor(Math.random() * (max - min + 1) + min);
  }
}
