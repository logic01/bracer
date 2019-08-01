import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ObservableStore } from '@codewithdan/observable-store';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { StoreState } from 'src/app/store/store-state';

import { environment } from '../../../environments/environment';
import { Physician } from '../../models/physician.model';

@Injectable({
  providedIn: 'root'
})
export class PhysicianService extends ObservableStore<StoreState>  {

  private url = `${environment.api_url}/physician`;

  constructor(private http: HttpClient) {
    super({ trackStateHistory: true });
  }

  get(id: string): Observable<Physician> {
    return this.http.get<Physician>(`${this.url}/${id}`);
  }

  getList(ids: string[]): Observable<Physician[]> {

    let queryString = '';

    ids.forEach(id => queryString += `\&ids=${id}`);

    return this.http.get<Physician[]>(`${this.url}?${queryString}`);
  }

  getAll(): Observable<Physician[]> {

    const state = this.getState();

    if (state && state.physicians) {
      return of(state.physicians);
    } else {
      return this.fetchAll();
    }
  }

  private fetchAll(): Observable<Physician[]> {

    return this.http
      .get<Physician[]>(this.url)
      .pipe(
        tap(newPhysicians => {
          this.setState({ physicians: newPhysicians }, 'physician-get-all');
        }));
  }

  post(physician: Physician): Observable<Physician> {
    return this.http.post<Physician>(this.url, physician);
  }

  put(id: string, physician: Physician): Observable<Physician> {
    return this.http.put<Physician>(`${this.url}/${id}`, physician);
  }
}
