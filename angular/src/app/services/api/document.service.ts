import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Document } from '../../models/document.model';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  constructor(private http: HttpClient) { }

  getAll(physicianId: string): Observable<Document[]> {
    return this.http.get<Document[]>(`${environment.api_url}/physician/${physicianId}/document`);
  }

  get(physicianId: string, documentId: string): Observable<Document[]> {
    return this.http.get<Document[]>(`${environment.api_url}/physician/${physicianId}/document/${documentId}`);
  }


  post(physicianId: string, document: Document): Observable<Document> {
    return this.http.post<Document>(`${environment.api_url}/physician/${physicianId}/document`, document);
  }

  put(physicianId: string, document: Document): Observable<Document> {
    return this.http.put<Document>(`${environment.api_url}/physician/${physicianId}/documents`, document);
  }
}
