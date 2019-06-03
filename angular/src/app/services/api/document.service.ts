import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Signature } from 'src/app/models/signature.model';

import { environment } from '../../../environments/environment';
import { Document } from '../../models/document.model';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  constructor(private http: HttpClient) { }

  getByPhysician(physicianId: string): Observable<Document[]> {
    return this.http.get<Document[]>(`${environment.api_url}/physician/${physicianId}/document`);
  }

  getByVendor(vendorId: string): Observable<Document[]> {
    return this.http.get<Document[]>(`${environment.api_url}/vendor/${vendorId}/document`);
  }

  get(documentId: string): Observable<Document> {
    return this.http.get<Document>(`${environment.api_url}/document/${documentId}`);
  }

  put(document: Document): Observable<Document> {
    return this.http.put<Document>(`${environment.api_url}/document/${document.documentId}`, document);
  }

  sign(documentId: string, signature: Signature): Observable<void> {
    return this.http.post<void>(`${environment.api_url}/document/${documentId}/signature`, signature);
  }
}
