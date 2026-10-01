import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-configuracao',
  standalone: true,
  templateUrl: './configuracao.html',
  styleUrl: './configuracao.css'
})

export class Configuracao {
  constructor(private router: Router) {}

  selecionarApi(): void {
    this.router.navigate(['/inicio']);
  }

  selecionarBancoLocal(): void {
    this.router.navigate(['/banco-local']);
  }
}