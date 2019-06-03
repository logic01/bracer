import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Vendor } from '../../models/vendor.model';

@Injectable({
  providedIn: 'root'
})
export class VendorService {

  private url = `${environment.api_url}/vendor`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Vendor[]> {
    return this.http.get<Vendor[]>(this.url);
  }

  get(id: string): Observable<Vendor> {
    return this.http.get<Vendor>(`${this.url}/${id}`);
  }

  post(vendor: Vendor): Observable<Vendor> {
    return this.http.post<Vendor>(this.url, vendor);
  }

  put(id: string, vendor: Vendor): Observable<Vendor> {
    return this.http.put<Vendor>(`${this.url}/${id}`, vendor);
  }
}
