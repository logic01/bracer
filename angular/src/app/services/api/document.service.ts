import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Document } from '../../models/document.model';
import { map } from 'rxjs/operators';

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

  download(documentId: string): Observable<any> {
    return this.http
      .get(`${environment.api_url}/document/${documentId}/download`, {
        responseType: 'blob'
      }).pipe(
        map((res: Blob) => {
          return {
            filename: 'filename.pdf',
            data: res
          };
        })
      );
  }
}
