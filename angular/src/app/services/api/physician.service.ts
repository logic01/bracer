import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Physician } from '../../models/physician.model';

@Injectable({
  providedIn: 'root'
})
export class PhysicianService {

  private url = `${environment.api_url}/physician`;

  constructor(private http: HttpClient) { }

  get(id: string): Observable<Physician> {
    return this.http.get<Physician>(`${this.url}/${id}`);
  }

  getList(ids: string[]): Observable<Physician[]> {

    let queryString = '';

    ids.forEach(id => queryString += `\&ids=${id}`);

    return this.http.get<Physician[]>(`${this.url}?${queryString}`);
  }

  getAll(): Observable<Physician[]> {
    return this.http.get<Physician[]>(this.url);
  }

  post(physician: Physician): Observable<Physician> {
    return this.http.post<Physician>(this.url, physician);
  }

  put(id: string, physician: Physician): Observable<Physician> {
    return this.http.put<Physician>(`${this.url}/${id}`, physician);
  }
}
