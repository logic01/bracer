import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ObservableStore } from '@codewithdan/observable-store';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Patient } from 'src/app/models/patient.model';
import { StoreState } from 'src/app/store/store-state';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PatientService extends ObservableStore<StoreState> {

  private url = `${environment.api_url}/patient`;

  constructor(private http: HttpClient) {
    super({ trackStateHistory: true });
  }

  get(id: string): Observable<Patient> {
    return this.http.get<Patient>(`${this.url}/${id}`);
  }

  getByAgent(agentId: string): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${environment.api_url}/agent/${agentId}/patient`);
  }

  getByVendor(vendorId: string): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${environment.api_url}/vendor/${vendorId}/patient`);
  }

  getList(ids: string[]): Observable<Patient[]> {

    let queryString = '';

    ids.forEach(id => queryString += `\&ids=${id}`);

    return this.http.get<Patient[]>(`${this.url}?${queryString}`);
  }

  getAll(): Observable<Patient[]> {

    const state = this.getState();

    if (state && state.patients) {
      return of(state.patients);
    } else {
      return this.fetchAll();
    }
  }

  private fetchAll(): Observable<Patient[]> {

    return this.http
      .get<Patient[]>(this.url)
      .pipe(
        tap((data: Patient[]) => {
          this.setState({ patients: data }, 'patient-get-all');
        }));
  }


  post(patient: Patient): Observable<{ patientId: string }> {
    return this.http.post<{ patientId: string }>(this.url, patient);
  }

  put(id: string, patient: Patient): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}`, patient);
  }
}
