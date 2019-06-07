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

  get(documentId: string): Observable<Document> {
    return this.http.get<Document>(`${environment.api_url}/document/${documentId}`);
  }

  put(document: Document): Observable<Document> {
    return this.http.put<Document>(`${environment.api_url}/document/${document.documentId}`, document);
  }

}
