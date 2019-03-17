import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Admin } from '../models/admin.model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private url = `${environment.api_url}/user`;

  constructor(private http: HttpClient) { }

  get() {
    return this.http.get(this.url);
  }

  post(admin: Admin): Observable<Admin> {
    return this.http.post<Admin>(this.url, admin);
  }

  put(admin: Admin): Observable<Admin> {
    return this.http.put<Admin>(this.url, admin);
  }

}
