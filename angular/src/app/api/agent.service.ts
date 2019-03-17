import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Agent } from '../models/agent.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AgentService {

  private url = `${environment.api_url}/agent`;

  constructor(private http: HttpClient) { }

  get() {
    return this.http.get(this.url);
  }

  post(admin: Agent): Observable<Agent> {
    return this.http.post<Agent>(this.url, admin);
  }

  put(admin: Agent): Observable<Agent> {
    return this.http.put<Agent>(this.url, admin);
  }

}
