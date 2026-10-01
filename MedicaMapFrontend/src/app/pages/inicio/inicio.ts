import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicio',
  standalone: true,
  templateUrl: './inicio.html',
  styleUrl: './inicio.css'
})

export class Inicio {

  constructor(private router: Router) {}

  visualizarConsulta(): void {
    this.router.navigate(['/consulta']);
  }

  configurarInformacoes(): void {
    this.router.navigate(['/configuracao']);
  }
}