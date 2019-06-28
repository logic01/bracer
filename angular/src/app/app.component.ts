import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

import { RouteUrls } from './constants/routes';
import { AccountType } from './models/enums/account-type.enum';
import { UserAccount } from './models/user-account.model';
import { LogoutService } from './services/logout.service';
import { SessionService } from './services/session.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public loggedIn$ = this.session.loggedIn$;
  public userAccount$ = this.session.userAccount$;

  constructor(
    private readonly router: Router,
    private readonly session: SessionService,
    private readonly logoutService: LogoutService) {

  }

  ngOnInit(): void {
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
  }

  singOutClick() {
    this.logoutService.logout();
  }

  home() {
    this.session.userAccount$.subscribe(
      (result: UserAccount) => {

        if (result.type === AccountType.Admin) {
          this.router.navigateByUrl(RouteUrls.AdminDashboardComponent);
        }

        if (result.type === AccountType.Agent) {
          this.router.navigateByUrl(RouteUrls.AgentDashboardComponent);
        }

        if (result.type === AccountType.Physician) {
          this.router.navigateByUrl(RouteUrls.PhysicianDashboardComponent);
        }
      });

  }

  billing() {
    this.router.navigateByUrl(RouteUrls.BillingDashboardComponent);
  }
}
