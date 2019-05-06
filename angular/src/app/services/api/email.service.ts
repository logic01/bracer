import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmailService {

  constructor(private http: HttpClient) { }

  post(documentId: string, emailAddress: string): Observable<boolean> {
    return this.http.post<boolean>(`${environment.api_url}/email`, { documentId: documentId, emailAddress: emailAddress });
  }
}
