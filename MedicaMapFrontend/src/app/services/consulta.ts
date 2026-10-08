
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfiguracaoService } from './configuracao';

@Injectable({
  providedIn: 'root'
})

export class ConsultaService {
  private readonly api = 'https://localhost:7164/api';

  constructor(private http: HttpClient,private configuracao: ConfiguracaoService) {}

  obterFonte(): 'api' | 'local' {
    return this.configuracao.obterFonte() ?? 'local';
  }

  obterEstados(): Observable<any[]> {
    if (this.obterFonte() === 'api') {
      return this.http.get<any[]>(`${this.api}/ibge/estados`);
    }

    return this.http.get<any[]>(`${this.api}/State`);
  }

  obterMunicipios(estado: any): Observable<any[]> {
    if (this.obterFonte() === 'api') {
      const codigoUf = estado.sigla ? estado.id : estado.ibgeCode;

      return this.http.get<any[]>(
        `${this.api}/ibge/estados/${codigoUf}/municipios`
      );
    }

    return this.http.get<any[]>(
      `${this.api}/Municipality/state/${estado.id}`
    );
  }

  obterEstabelecimentos(estado: any, municipio: any): Observable<any[]> {
    if (this.obterFonte() === 'api') {
      const codigoUf = estado.id;
      const codigoMunicipio = municipio.id;

      return this.http.get<any[]>(
        `${this.api}/cnes/estabelecimentos`,
        { params: {
            codigoUf: String(codigoUf),
            codigoMunicipio: String(codigoMunicipio)
          }
        }
      );
    }

    return this.http.get<any[]>(
      `${this.api}/Establishment/municipality/${municipio.id}`
    );
  }

  obterEstoques(estado: any, municipio: any, estabelecimento: any): Observable<any[]> {
    if (this.obterFonte() === 'api') {
      return this.http.get<any[]>(
        `${this.api}/bnafar/estoques`,
        {
          params: {
            codigoUf: String(estado.id),
            codigoMunicipio: String(municipio.id),
            codigoCnes: String(
              estabelecimento.codigoCnes ??
              estabelecimento.cnesCode ??
              estabelecimento.codigo_cnes
            )
          }
        }
      );
    }

    return this.http.get<any[]>(
      `${this.api}/Stock/establishment/${estabelecimento.id}`
    );
  }
}