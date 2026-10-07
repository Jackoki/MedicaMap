import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { ConfiguracaoService } from '../../services/configuracao';

@Component({
  selector: 'app-configuracao',
  standalone: true,
  templateUrl: './configuracao.html',
  styleUrl: './configuracao.css'
})

export class Configuracao {
  constructor(private router: Router, private location: Location, private configuracaoService: ConfiguracaoService) {}

  selecionarApi(): void {
    this.configuracaoService.definirFonte('api');
    this.router.navigate(['/inicio']);
  }

  selecionarBancoLocal(): void {
    this.router.navigate(['/banco-local']);
  }

  voltar(): void {
    this.location.back();
  }
}