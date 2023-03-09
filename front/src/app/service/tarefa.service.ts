import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';

import { IRetornoApi, RetornoApi } from '../model/retorno';
import { Tarefa } from '../model/tarefa';

@Injectable({
    providedIn: 'root'
})
export class TarefaService  {

  protected httpHeaders: HttpHeaders;

    constructor(protected http: HttpClient) {
        this.httpHeaders = new HttpHeaders({ Accept: "application/json ; text/plain", "Content-type": "application/json; charset=utf-8" });
    }

    public consultar(): Observable<RetornoApi<Tarefa>> {
        return this.http.get<RetornoApi<Tarefa>>(`${environment.apiUrl}/tarefas`);
    }
    public consultarPorId(id: string): Observable<RetornoApi<Tarefa>> {
        return this.http.get<RetornoApi<Tarefa>>(`${environment.apiUrl}/tarefas/${id}`);
    }
    public salvar(tarefa: Tarefa): Observable<IRetornoApi> {
        return this.http.post<IRetornoApi>(`${environment.apiUrl}/tarefas`, tarefa, { headers: this.httpHeaders });
    }
}
