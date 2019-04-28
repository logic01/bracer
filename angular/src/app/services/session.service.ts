import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import { AccountType } from '../models/enums/account-type.enum';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  public accountType$: BehaviorSubject<AccountType> = new BehaviorSubject<AccountType>(AccountType.None);

}
