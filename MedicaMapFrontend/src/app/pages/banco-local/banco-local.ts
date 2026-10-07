import { Component } from '@angular/core';
import { BnafarService } from '../../services/bnafar.service';

@Component({
  selector: 'app-banco-local',
  standalone: true,
  templateUrl: './banco-local.html',
  styleUrl: './banco-local.css'
})

export class BancoLocal {
  nomeArquivo: string = '';
  arquivo: File | null = null;

  constructor(private bnafarService: BnafarService) {}

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

    this.bnafarService.importarArquivo(this.arquivo)
      .subscribe({
        next: resposta => {
          console.log('Importação:', resposta);
        },
        error: erro => {
          console.error('Erro ao enviar arquivo:', erro);
        }
      });
  }

  instalarBanco(): void {
    console.log('Instalar banco de dados');
  }

  removerBanco(): void {
    console.log('Remover banco de dados');
  }
}