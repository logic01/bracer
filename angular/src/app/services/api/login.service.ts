import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { UserAccount } from '../../models/user-account.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private url = `${environment.api_url}/login`;

  constructor(private http: HttpClient) { }

  post(user: UserAccount): Observable<UserAccount> {
    return this.http.post<UserAccount>(this.url, user);
  }
}
