import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Admin } from '../../models/admin.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private url = `${environment.api_url}/admin`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Admin[]> {
    return this.http.get<Admin[]>(this.url);
  }

  get(id: string): Observable<Admin> {
    return this.http.get<Admin>(`${this.url}/${id}`);
  }

  post(admin: Admin): Observable<Admin> {
    return this.http.post<Admin>(this.url, admin);
  }

  put(id: string, admin: Admin): Observable<{ adminId: string }> {
    return this.http.put<{ adminId: string }>(`${this.url}/${id}`, admin);
  }
}
