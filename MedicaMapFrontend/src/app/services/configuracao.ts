import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class ConfiguracaoService {
  private readonly chave = 'fonteDados';

  definirFonte(fonte: 'api' | 'local'): void {
    localStorage.setItem(this.chave, fonte);
  }

  obterFonte(): 'api' | 'local' | null {
    const fonte = localStorage.getItem(this.chave);

    if (fonte === 'api' || fonte === 'local') {
      return fonte;
    }

    return null;
  }
}