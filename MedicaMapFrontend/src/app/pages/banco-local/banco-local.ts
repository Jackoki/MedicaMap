import { Component, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { Subscription, timer, of } from 'rxjs';
import { catchError, filter, switchMap, takeWhile} from 'rxjs/operators';
import { BnafarService, ImportProgress } from '../../services/bnafar.service';
import { ConfiguracaoService } from '../../services/configuracao';

@Component({
  selector: 'app-banco-local',
  standalone: true,
  templateUrl: './banco-local.html',
  styleUrl: './banco-local.css'
})

export class BancoLocal implements OnDestroy {
  nomeArquivo = '';
  nomeArquivoEstabelecimentos = '';
  nomeArquivoEstoques = '';

  importando = false;
  mensagem = '';
  erro = '';

  etapaImportacao = '';
  percentualImportacao = 0;

  arquivoEstabelecimentos: File | null = null;
  arquivoEstoques: File | null = null;
  operacaoBancoEmAndamento = false;
  fonteSelecionada: 'api' | 'local' = 'api';

  private progressoSubscription?: Subscription;

  constructor(private bnafarService: BnafarService, private configuracaoService: ConfiguracaoService, private router: Router, private location: Location, private cdr: ChangeDetectorRef) {
    this.fonteSelecionada = this.configuracaoService.obterFonte() ?? 'api';
  }

  arquivoEstabelecimentosSelecionado(event: Event): void {
    const input = event.target as HTMLInputElement;
    const arquivo = input.files?.[0];

    if (!arquivo) {
      return;
    }

    this.arquivoEstabelecimentos = arquivo;
    this.nomeArquivoEstabelecimentos = arquivo.name;

    this.limparMensagens();
  }

  arquivoEstoquesSelecionado(event: Event): void {
    const input = event.target as HTMLInputElement;
    const arquivo = input.files?.[0];

    if (!arquivo) {
      return;
    }

    this.arquivoEstoques = arquivo;
    this.nomeArquivoEstoques = arquivo.name;

    this.limparMensagens();
  }

  importarEstabelecimentos(): void {
    if (!this.arquivoEstabelecimentos || this.importando || this.operacaoBancoEmAndamento) {
      return;
    }

    this.iniciarImportacao(
      this.arquivoEstabelecimentos,
      this.nomeArquivoEstabelecimentos,
      (id) =>
        this.bnafarService.importarEstabelecimentosArquivo(this.arquivoEstabelecimentos!, id),
      'Ocorreu um erro durante a importação dos estabelecimentos.'
    );
  }

  importarEstoques(): void {
    if (!this.arquivoEstoques || this.importando || this.operacaoBancoEmAndamento) {
      return;
    }

    this.iniciarImportacao(this.arquivoEstoques, this.nomeArquivoEstoques,
      (id) =>
        this.bnafarService.importarArquivo(
          this.arquivoEstoques!,
          id
        ),
      'Ocorreu um erro durante a importação dos estoques.'
    );
  }

  private iniciarImportacao(arquivo: File, nomeArquivo: string, importar: (id: string) => import('rxjs').Observable<{ mensagem: string; importacaoId: string; }>, mensagemErro: string): void {
    this.importando = true;
    this.nomeArquivo = nomeArquivo;
    this.etapaImportacao = 'Enviando arquivo...';
    this.percentualImportacao = 0;

    this.limparMensagens();
    this.pararAcompanhamento();

    const importacaoId = crypto.randomUUID();

    this.acompanharProgresso(importacaoId);

    importar(importacaoId).subscribe({
      next: (resposta) => {
        console.log('Resposta da importação:', resposta);
        this.mensagem = resposta?.mensagem || 'Requisição de importação concluída.';
        this.cdr.detectChanges();
      },

      error: (erro) => {
        this.pararAcompanhamento();
        this.importando = false;
        this.erro = erro.error?.mensagem || erro.error?.message || mensagemErro;
        this.cdr.detectChanges();
      }
    });
  }

  private acompanharProgresso(importacaoId: string): void {
    this.progressoSubscription = timer(0, 1000)
      .pipe(
        switchMap(() =>
          this.bnafarService.consultarProgresso(importacaoId).pipe(
            catchError((erro) => {
              console.error('Erro ao consultar progresso:', erro);
              return of(null);
            })
          )
        ),
        filter((progresso): progresso is ImportProgress => progresso !== null),
        takeWhile((progresso) => !progresso.concluido, true)
      )
      .subscribe((progresso) => {
        console.log('Progresso retornado pela API:', progresso);
        this.etapaImportacao = progresso.etapa;
        this.percentualImportacao = progresso.percentual;

        if (progresso.concluido) {
          this.importando = false;
          if (progresso.sucesso) {
            this.etapaImportacao = 'Importação concluída';
            this.percentualImportacao = 100;
          } 

          else {
            this.erro = progresso.erro ?? 'A importação não foi concluída.';
          }
          this.pararAcompanhamento();
        }

        this.cdr.detectChanges();
      });
  }

  private pararAcompanhamento(): void {
    this.progressoSubscription?.unsubscribe();
    this.progressoSubscription = undefined;
  }

    
  instalarBanco(): void {
    this.limparMensagens();
    console.log('Iniciando instalação do banco...');
    this.bnafarService.instalarBanco().subscribe({
      next: (resposta) => {
        console.log('Resposta da instalação:', resposta);
        this.mensagem = resposta?.mensagem || 'Banco de dados instalado com sucesso.';
        this.erro = '';
        this.cdr.detectChanges();
        console.log('Mensagem exibida:', this.mensagem);
      },

      error: (erro) => {
        console.error('Erro ao instalar banco:', erro);
        this.mensagem = '';
        this.erro = erro.error?.mensagem || erro.error?.message || 'Ocorreu um erro ao instalar o banco de dados.';
        this.cdr.detectChanges();
      }
    });
  }

  removerBanco(): void {
    const confirmar = confirm('Tem certeza que deseja remover o banco de dados? Todos os dados serão apagados.');
    if (!confirmar) {
      return;
    }

    this.limparMensagens();
    this.bnafarService.removerBanco().subscribe({
      next: (resposta) => {
        console.log('Resposta da API:', resposta);
        this.mensagem = resposta?.mensagem || 'Banco de dados removido com sucesso.';
        this.erro = '';
        this.cdr.detectChanges();
        console.log('Mensagem exibida:', this.mensagem);
      },

      error: (erro) => {
        console.error('Erro ao remover banco:', erro);
        this.mensagem = '';
        this.erro = erro.error?.mensagem || erro.error?.message || 'Ocorreu um erro ao remover o banco de dados.';
        this.cdr.detectChanges();
      }
    });
  }

  utilizarApi(): void {
    if (this.importando || this.operacaoBancoEmAndamento) {
      return;
    }

    this.configuracaoService.definirFonte('api');
    this.fonteSelecionada = 'api';
  }

  utilizarBancoLocal(): void {
    if (this.importando || this.operacaoBancoEmAndamento) {
      return;
    }

    this.configuracaoService.definirFonte('local');
    this.fonteSelecionada = 'local';
  }

  voltar(): void {  
    this.router.navigate(['/inicio']);
  }

  private limparMensagens(): void {
    this.mensagem = '';
    this.erro = '';
  }

  ngOnDestroy(): void {
    this.pararAcompanhamento();
  }
}