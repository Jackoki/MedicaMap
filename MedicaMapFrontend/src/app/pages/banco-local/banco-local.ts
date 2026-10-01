import { Component } from '@angular/core';

@Component({
  selector: 'app-banco-local',
  standalone: true,
  templateUrl: './banco-local.html',
  styleUrl: './banco-local.css'
})

export class BancoLocal {
  importarPlanilha(): void {
    console.log('Importar planilha');
  }

  instalarBanco(): void {
    console.log('Instalar banco de dados');
  }

  removerBanco(): void {
    console.log('Remover banco de dados');
  }
}