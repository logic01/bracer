import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Call } from '../models/call.model';
import { environment } from '../../environments/environment';

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
