import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Call } from '../../models/call.model';

@Injectable({
  providedIn: 'root'
})
export class CallService {

  private url = `${environment.api_url}/call`;

  constructor(private http: HttpClient) { }

  get() {
    return this.http.get(this.url);
  }

  post(admin: Call): Observable<Call> {
    return this.http.post<Call>(this.url, admin);
  }

  put(admin: Call): Observable<Call> {
    return this.http.put<Call>(this.url, admin);
  }
}
