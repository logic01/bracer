import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';

import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { RouteUrls } from '../constants/routes';
import { AccountType } from '../models/Enums/account.type.enum';
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

    return this.session.accountType$
      .pipe(
        take(1),
        map((type: AccountType) => {

          if (type === expectedRole) {
            return true;
          }

          this.router.navigateByUrl(RouteUrls.LoginComponent);
          return false;
        })
      );

  }
}
