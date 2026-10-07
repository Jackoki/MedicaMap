import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class BnafarService {
  private readonly url = 'https://localhost:7164/api/bnafar/importar';
  constructor(private http: HttpClient) {}

  importarArquivo(arquivo: File): Observable<any> {
    const formData = new FormData();
    formData.append('arquivo', arquivo);
    return this.http.post(this.url, formData);
  }
}