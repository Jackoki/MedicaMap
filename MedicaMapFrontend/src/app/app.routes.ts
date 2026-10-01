import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'inicio',
    pathMatch: 'full'
  },
  {
    path: 'inicio',
    loadComponent: () => import('./pages/inicio/inicio').then(m => m.Inicio)
  },
  {
    path: 'configuracao',
    loadComponent: () => import('./pages/configuracao/configuracao').then(m => m.Configuracao)
  },
  {
    path: 'banco-local',
    loadComponent: () => import('./pages/banco-local/banco-local').then(m => m.BancoLocal)
  },
  {
    path: 'consulta',
    loadComponent: () => import('./pages/consulta/consulta').then(m => m.Consulta)
  }
];