import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfiguracaoService } from './configuracao';

@Injectable({
  providedIn: 'root'
})

export class ConsultaService {
  private readonly api = 'https://localhost:7164/api';

  constructor(private http: HttpClient, private configuracao: ConfiguracaoService) {}

  obterFonte(): 'api' | 'local' {
    return this.configuracao.obterFonte() ?? 'local';
  }

  obterEstados(): Observable<any[]> {
    return this.http.get<any[]>(`${this.api}/State`);
  }

  obterMunicipios(estado: any): Observable<any[]> {
    return this.http.get<any[]>(`${this.api}/Municipality/state/${estado.id}`);
  }

  obterEstabelecimentos(estado: any, municipio: any): Observable<any[]> {
    return this.http.get<any[]>(`${this.api}/Establishment/municipality/${municipio.id}`);
  }

  obterEstoques(estado: any, municipio: any, estabelecimento: any): Observable<any[]> {
    if (this.obterFonte() === 'api') {
      return this.http.get<any[]>(`${this.api}/bnafar/estoques`,
        {
          params: {
            codigoUf: String(estado.ibgeCode ?? estado.codigoIbge ?? estado.id),
            codigoMunicipio: String(municipio.ibgeCode ?? municipio.codigoIbge ?? municipio.id),
            codigoCnes: String(estabelecimento.codigoCnes ?? estabelecimento.cnesCode ?? estabelecimento.codigo_cnes)
          }
        }
      );
    }

    return this.http.get<any[]>(`${this.api}/Stock/establishment/${estabelecimento.id}`);
  }
}