import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';

import { IRetornoApi, RetornoApi } from '../model/retorno';
import { Tarefa } from '../model/tarefa';

@Injectable({
  providedIn: 'root'
})
export class TarefaService {

  protected httpHeaders: HttpHeaders;

  constructor(protected http: HttpClient) {
    this.httpHeaders = new HttpHeaders({ Accept: "application/json ; text/plain", "Content-type": "application/json; charset=utf-8" });
  }

  public obterTarefas(): Observable<RetornoApi<Tarefa>> {
    return this.http.get<RetornoApi<Tarefa>>(`${environment.apiUrl}/tarefas`);
  }
  public obterTarefaPorCodigo(codigo: string): Observable<RetornoApi<Tarefa>> {
    return this.http.get<RetornoApi<Tarefa>>(`${environment.apiUrl}/tarefas/${codigo}`);
  }
  public incluir(tarefa: Tarefa): Observable<IRetornoApi> {
    return this.http.post<IRetornoApi>(`${environment.apiUrl}/tarefas`, tarefa, { headers: this.httpHeaders });
  }
  public alterar(tarefa: Tarefa): Observable<IRetornoApi> {
    return this.http.put<IRetornoApi>(`${environment.apiUrl}/tarefas`, tarefa, { headers: this.httpHeaders });
  }
  public excluir(codigos: number[]): Observable<IRetornoApi> {
    let params = codigos.map(codigo => `codigo=${codigo}`).join('&');
    return this.http.delete<IRetornoApi>(`${environment.apiUrl}/tarefas?${params}`);
  }
}
