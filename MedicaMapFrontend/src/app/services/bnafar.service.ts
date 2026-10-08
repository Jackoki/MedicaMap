import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class BnafarService {
  private readonly url = 'https://localhost:7164/api/bnafar';
  constructor(private http: HttpClient) {}

  importarArquivo(arquivo: File): Observable<any> {
    const formData = new FormData();
    formData.append('arquivo', arquivo);
    return this.http.post(`${this.url}/importar`, formData);
  }

  instalarBanco(): Observable<any> {
    return this.http.post(`${this.url}/instalar-banco`, {});
  }

  removerBanco(): Observable<any> {
    return this.http.delete(`${this.url}/remover-banco`);
  }
}