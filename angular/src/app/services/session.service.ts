import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { UserAccount } from '../models/user-account.model';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  public userAccount$: BehaviorSubject<UserAccount> = new BehaviorSubject<UserAccount>(new UserAccount());
}
