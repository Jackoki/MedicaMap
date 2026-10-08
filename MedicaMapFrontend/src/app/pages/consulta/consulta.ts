
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConsultaService } from '../../services/consulta';

@Component({
  selector: 'app-consulta',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './consulta.html',
  styleUrl: './consulta.css'
})

export class Consulta implements OnInit {
  estados: any[] = [];
  municipios: any[] = [];
  estabelecimentos: any[] = [];
  estoques: any[] = [];

  estadoSelecionado: any = null;
  municipioSelecionado: any = null;
  estabelecimentoSelecionado: any = null;

  carregando = false;
  erro = '';
  fonte: 'api' | 'local';

  constructor(private consultaService: ConsultaService) {
    this.fonte = this.consultaService.obterFonte();
  }

  ngOnInit(): void {
    this.carregarEstados();
  }

  carregarEstados(): void {
    this.carregando = true;
    this.erro = '';

    this.consultaService.obterEstados().subscribe({
      next: dados => {
        this.estados = dados;
        this.carregando = false;
      },
      error: err => this.tratarErro(err)
    });
  }

  selecionarEstado(): void {
    this.municipios = [];
    this.estabelecimentos = [];
    this.estoques = [];

    this.municipioSelecionado = null;
    this.estabelecimentoSelecionado = null;

    if (!this.estadoSelecionado) return;

    this.carregando = true;
    this.erro = '';

    this.consultaService.obterMunicipios(this.estadoSelecionado).subscribe({
        next: dados => {
          this.municipios = dados;
          this.carregando = false;
        },
        error: err => this.tratarErro(err)
      });
  }

  selecionarMunicipio(): void {
    this.estabelecimentos = [];
    this.estoques = [];
    this.estabelecimentoSelecionado = null;

    if (!this.municipioSelecionado) return;

    this.carregando = true;
    this.erro = '';

    this.consultaService.obterEstabelecimentos(this.estadoSelecionado, this.municipioSelecionado).subscribe({
        next: dados => {
          this.estabelecimentos = dados;
          this.carregando = false;
        },
        error: err => this.tratarErro(err)
      });
  }

  selecionarEstabelecimento(): void {
    this.estoques = [];

    if (!this.estabelecimentoSelecionado) return;

    this.carregando = true;
    this.erro = '';

    this.consultaService.obterEstoques(this.estadoSelecionado, this.municipioSelecionado, this.estabelecimentoSelecionado).subscribe({
        next: dados => {
          this.estoques = dados;
          this.carregando = false;
        },
        error: err => this.tratarErro(err)
      });
  }

  private tratarErro(err: any): void {
    this.erro = 'Não foi possível carregar os dados. Verifique a conexão e tente novamente.';
    this.carregando = false;
  }
}