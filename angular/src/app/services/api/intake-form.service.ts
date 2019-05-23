import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { IntakeForm } from '../../models/intake-form.model';

@Injectable({
  providedIn: 'root'
})
export class IntakeFormService {

  private url = `${environment.api_url}/IntakeForm`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<IntakeForm[]> {
    return this.http.get<IntakeForm[]>(this.url);
  }

  get(id: string): Observable<IntakeForm> {
    return this.http.get<IntakeForm>(`${this.url}/${id}`);
  }

  post(intakeForm: IntakeForm): Observable<IntakeForm> {
    return this.http.post<IntakeForm>(this.url, intakeForm);
  }

  put(intakeForm: IntakeForm): Observable<IntakeForm> {
    return this.http.put<IntakeForm>(this.url, intakeForm);
  }
}