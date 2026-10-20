
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConsultaService } from '../../services/consulta';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';

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

  carregandoEstados = false;
  carregandoMunicipios = false;
  carregandoEstabelecimentos = false;

  constructor(private consultaService: ConsultaService, private router: Router, private cdr: ChangeDetectorRef) {
    this.fonte = this.consultaService.obterFonte();
  }

  ngOnInit(): void {
    this.carregarEstados();
  }

  carregarEstados(): void {
    this.carregando = true;
    this.carregandoEstados = true;
    this.erro = '';

    this.consultaService.obterEstados().subscribe({
      next: dados => {
        this.estados = dados;
        this.carregando = false;
        this.carregandoEstados = false;

        this.cdr.detectChanges();
      },
      error: err => {
        this.carregando = false;
        this.carregandoEstados = false;
        this.tratarErro(err);

        this.cdr.detectChanges();
      }
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
    this.carregandoMunicipios = true;
    this.erro = '';

    this.consultaService.obterMunicipios(this.estadoSelecionado)
      .pipe(
        finalize(() => {
          this.carregando = false;
          this.carregandoMunicipios = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: dados => {
          this.municipios = dados;
        },
        error: err => {
          this.tratarErro(err);
        }
      });
  }

  selecionarMunicipio(): void {
    this.estabelecimentos = [];
    this.estoques = [];
    this.estabelecimentoSelecionado = null;

    if (!this.municipioSelecionado) return;

    this.carregando = true;
    this.carregandoEstabelecimentos = true;
    this.erro = '';

    this.consultaService.obterEstabelecimentos(this.estadoSelecionado, this.municipioSelecionado)
      .pipe(
        finalize(() => {
          this.carregando = false;
          this.carregandoEstabelecimentos = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: dados => {
          this.estabelecimentos = dados;
        },
        error: err => {
          this.tratarErro(err);
        }
      });
  }

  selecionarEstabelecimento(): void {
    this.estoques = [];

    if (!this.estabelecimentoSelecionado) return;

    this.carregando = true;
    this.erro = '';

    this.consultaService.obterEstoques(this.estadoSelecionado, this.municipioSelecionado, this.estabelecimentoSelecionado)
      .pipe(
        finalize(() => {
          this.carregando = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: dados => {
          this.estoques = dados;
        },
        error: err => {
          this.tratarErro(err);
        }
      });
  }

  private tratarErro(err: any): void {
    this.erro = 'Não foi possível carregar os dados. Verifique a conexão e tente novamente.';
    this.carregando = false;
  }

  voltar(): void {
    this.router.navigate(['/inicio']);
  }
}