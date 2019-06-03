import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { Patient } from 'src/app/models/patient.model';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class PatientService {

  private url = `${environment.api_url}/patient`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.url);
  }

  get(id: string): Observable<Patient> {
    return this.http.get<Patient>(`${this.url}/${id}`);
  }

  post(patient: Patient): Observable<Patient> {
    return this.http.post<Patient>(this.url, patient);
  }

  put(id: string, patient: Patient): Observable<Patient> {
    return this.http.put<Patient>(`${this.url}/${id}`, patient);
  }
}
