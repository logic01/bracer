import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';

import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { RouteUrls } from '../constants/routes';
import { AccountType } from '../models/enums/account-type.enum';
import { UserAccount } from '../models/user-account.model';
import { SessionService } from './session.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService implements CanActivate {

  constructor(
    private readonly router: Router,
    private readonly session: SessionService) {
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {

    const expectedRole = next.data.expectedRole as AccountType;

    return this.session.userAccount$.pipe(
      take(1),
      map((account: UserAccount) => {

        if (account.type === expectedRole) {
          return true;
        }

        this.router.navigateByUrl(RouteUrls.LoginComponent);
        return false;
      })
    );

  }
}
