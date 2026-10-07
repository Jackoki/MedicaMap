import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { BnafarService } from '../../services/bnafar.service';
import { ConfiguracaoService } from '../../services/configuracao';

@Component({
  selector: 'app-banco-local',
  standalone: true,
  templateUrl: './banco-local.html',
  styleUrl: './banco-local.css'
})

export class BancoLocal {
  nomeArquivo: string = '';
  importando = false;
  mensagem = '';
  erro = '';
  arquivo: File | null = null;

  constructor(private bnafarService: BnafarService, private configuracaoService: ConfiguracaoService, private router: Router, private location: Location) {}

  arquivoSelecionado(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (!input.files || input.files.length === 0) {
      return;
    }

    this.arquivo = input.files[0];
    this.nomeArquivo = this.arquivo.name;
  }

  importarPlanilha(): void {
    if (!this.arquivo) {
      return;
    }

    this.importando = true;
    this.mensagem = '';
    this.erro = '';

    this.bnafarService.importarArquivo(this.arquivo).subscribe({
      next: (resposta) => {
        this.importando = false;
        this.mensagem = resposta.mensagem;
      },

      error: (erro) => {
        this.importando = false;
        this.erro = 'Ocorreu um erro durante a importação do arquivo.';
        console.error(erro);
      }
    });
  }

  instalarBanco(): void {
    console.log('Instalar banco de dados');
  }

  removerBanco(): void {
    console.log('Remover banco de dados');
  }

  utilizarBancoLocal(): void {
    this.configuracaoService.definirFonte('local');
    this.router.navigate(['/inicio']);
  }

  voltar(): void {
    this.location.back();
  }
}