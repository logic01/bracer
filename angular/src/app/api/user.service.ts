import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private url = `${environment.api_url}/users`;

  constructor(private http: HttpClient) { }

  get(): Observable<User> {
    return this.http.get<User>(this.url);
  }

  post(user: User): Observable<User> {
    return this.http.post<User>(this.url, user);
  }

  put(user: User): Observable<User> {
    return this.http.put<User>(this.url, user);
  }

}
