import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {

  menu: any[] = [
    {
      title: 'Dashboard',
      icon: 'mdi mdi-gauge',
      submenu: [
        { title: 'Main', url: '/' },
        { title: 'ProgressBar', url: '/dashboard/progress' },
        { title: 'Graficas', url: '/dashboard/grafica1' },
        { title: 'Promesas', url: '/dashboard/promesas' },
        { title: 'Juegos Azar', url: '/dashboard/juegos-azar'},
        { title: 'rxJS', url: '/dashboard/rxjs'},
      ]
    },
    {
      title: 'Operaciones',
      icon: 'mdi mdi-gauge',
      submenu: [
        { title: 'Calculo de Tarifas', url: '/operaciones/calculo-tarifas' },
        { title: 'Evaluacion de Servicio', url: '/operaciones/evaluacion-servicio' },
        { title: 'Creacion de Servicio', url: '/operaciones/creacion-servicio' },        
      ]
    }
  ];

  constructor() { }
}
