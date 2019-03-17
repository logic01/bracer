import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private url = `${environment.api_url}/login`;

  constructor(private http: HttpClient) { }

  post(user: User): Observable<User> {
    return this.http.post<User>(this.url, user);
  }
}
