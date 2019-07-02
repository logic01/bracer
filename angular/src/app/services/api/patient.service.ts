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

  getByAgent(agentId: string): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${environment.api_url}/agent/${agentId}/patient`);
  }

  getByVendor(vendorId: string): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${environment.api_url}/vendor/${vendorId}/patient`);
  }

  get(id: string): Observable<Patient> {
    return this.http.get<Patient>(`${this.url}/${id}`);
  }

  post(patient: Patient): Observable<{ patientId: string }> {
    return this.http.post<{ patientId: string }>(this.url, patient);
  }

  put(id: string, patient: Patient): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}`, patient);
  }
}
