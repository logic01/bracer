import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Document } from '../../models/document.model';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private url = `${environment.api_url}/document`;

  constructor(private http: HttpClient) { }

  get() {
    return this.http.get(this.url);
  }

  post(admin: Document): Observable<Document> {
    return this.http.post<Document>(this.url, admin);
  }

  put(admin: Document): Observable<Document> {
    return this.http.put<Document>(this.url, admin);
  }
}
