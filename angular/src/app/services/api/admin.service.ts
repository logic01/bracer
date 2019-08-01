import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ObservableStore } from '@codewithdan/observable-store';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { StoreState } from 'src/app/store/store-state';

import { environment } from '../../../environments/environment';
import { Admin } from '../../models/admin.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService extends ObservableStore<StoreState> {

  private url = `${environment.api_url}/admin`;

  constructor(private http: HttpClient) {
    super({ trackStateHistory: true });
  }


  getAll(): Observable<Admin[]> {

    const state = this.getState();

    if (state && state.admins) {
      return of(state.admins);
    } else {
      return this.fetchAll();
    }
  }

  private fetchAll(): Observable<Admin[]> {
    return this.http
      .get<Admin[]>(this.url)
      .pipe(
        tap((data: Admin[]) => this.setState({ admins: data }, 'admins-get-all'))
      );
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
