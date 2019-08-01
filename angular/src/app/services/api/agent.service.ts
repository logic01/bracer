import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ObservableStore } from '@codewithdan/observable-store';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { StoreState } from 'src/app/store/store-state';

import { environment } from '../../../environments/environment';
import { Agent } from '../../models/Agent.model';

@Injectable({
  providedIn: 'root'
})
export class AgentService extends ObservableStore<StoreState> {

  private url = `${environment.api_url}/agent`;

  constructor(private http: HttpClient) {
    super({ trackStateHistory: true });
  }


  getList(ids: string[]): Observable<Agent[]> {

    let queryString = '';

    ids.forEach(id => queryString += `\&ids=${id}`);

    return this.http.get<Agent[]>(`${this.url}?${queryString}`);
  }

  getAll(): Observable<Agent[]> {

    const state = this.getState();

    if (state && state.agents) {
      return of(state.agents);
    } else {
      return this.fetchAll();
    }
  }

  private fetchAll(): Observable<Agent[]> {
    return this.http
      .get<Agent[]>(this.url)
      .pipe(
        tap(newAgents => this.setState({ agents: newAgents }, 'agent-get-all'))
      );
  }

  get(id: string): Observable<Agent> {
    return this.http.get<Agent>(`${this.url}/${id}`);
  }

  post(agent: Agent): Observable<Agent> {
    return this.http.post<Agent>(this.url, agent);
  }

  put(id: string, agent: Agent): Observable<Agent> {
    return this.http.put<Agent>(`${this.url}/${id}`, agent);
  }
}
