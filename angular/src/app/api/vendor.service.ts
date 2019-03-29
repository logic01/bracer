import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vendor } from '../models/vendor.model';
import { environment } from '../../environments/environment';

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

  post(admin: Vendor): Observable<Vendor> {
    return this.http.post<Vendor>(this.url, admin);
  }

  put(admin: Vendor): Observable<Vendor> {
    return this.http.put<Vendor>(this.url, admin);
  }
}
