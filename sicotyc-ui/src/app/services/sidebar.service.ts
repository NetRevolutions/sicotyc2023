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
      icon: 'mdi mdi-memory',
      submenu: [
        { title: 'Calculo de Tarifas', url: '/operaciones/calculo-tarifas' },
        { title: 'Evaluacion de Servicio', url: '/operaciones/evaluacion-servicio' },
        { title: 'Creacion de Servicio', url: '/operaciones/creacion-servicio' },        
      ]
    },
    {
      title: 'Mantenimientos',
      icon: 'mdi mdi-folder-lock-open',
      submenu: [
        { title: 'Usuarios', url: '/mantenimientos/users' },
        { title: 'Lookup Code Groups', url: '/mantenimientos/lookupCodeGroups'},
        { title: 'Lookup Codes', url: '/mantenimientos/lookupCodeGroups/lookupCodes'}         
      ]
    },
    {
      title: 'Tutoriales',
      icon: 'mdi mdi-folder-lock-open',
      submenu: [
        { title: 'Conociendo SICOTYC', url: '/' },
        { title: 'Solicitar Servicios', url: '/' },
        { title: 'Seguimiento de Operaciones', url: '/' },
        { title: 'Buscar Transporte', url: '/' },
        { title: 'Registar Choferes', url: '/' },
        { title: 'Registrar Unidades y/o Complementos', url: '/' },
        { title: 'Solicitar SCTR', url: '/' },
        { title: 'Tramites DPW', url: '/' },
        { title: 'Tramites APM', url: '/' },
      ]
    }
  ];

  constructor() { }
}
