import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ImportProgress {
  id: string;
  etapa: string;
  percentual: number;
  concluido: boolean;
  sucesso: boolean;
  erro: string | null;
}

export interface ImportResponse {
  mensagem: string;
  importacaoId: string;
}

@Injectable({
  providedIn: 'root'
})

export class BnafarService {
  private readonly url = 'https://localhost:7164/api/bnafar';
  constructor(private http: HttpClient) {}

  importarArquivo(arquivo: File, importacaoId: string): Observable<ImportResponse> {
    const formData = new FormData();
    formData.append('arquivo', arquivo);
    formData.append('importacaoId', importacaoId);

    return this.http.post<ImportResponse>(`${this.url}/importar`, formData);
  }

  importarEstabelecimentosArquivo(arquivo: File, importacaoId: string): Observable<ImportResponse> {
    const formData = new FormData();
    formData.append('arquivo', arquivo);
    formData.append('importacaoId', importacaoId);

    return this.http.post<ImportResponse>(`${this.url}/importar-estabelecimentos`, formData);
  }

  consultarProgresso(importacaoId: string): Observable<ImportProgress> {
    return this.http.get<ImportProgress>(`${this.url}/progresso/${importacaoId}`);
  }

  instalarBanco(): Observable<{ mensagem: string }> {
    return this.http.post<{ mensagem: string }>(`${this.url}/instalar-banco`, {});
  }

  removerBanco(): Observable<{ mensagem: string }> {
    return this.http.delete<{ mensagem: string }>(`${this.url}/remover-banco`);
  }
}