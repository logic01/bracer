import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Physician } from '../models/physician.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PhyisicianService {

  private url = `${environment.api_url}/phyisician`;

  constructor(private http: HttpClient) { }

  get() {
    return this.http.get(this.url);
  }

  post(admin: Physician): Observable<Physician> {
    return this.http.post<Physician>(this.url, admin);
  }

  put(admin: Physician): Observable<Physician> {
    return this.http.put<Physician>(this.url, admin);
  }
}
