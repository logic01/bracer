import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Agent } from 'src/app/models/Agent.model';
import { AccountType } from 'src/app/models/enums/account-type.enum';
import { UserAccount } from 'src/app/models/user-account.model';
import { Vendor } from 'src/app/models/vendor.model';
import { CustomValidators } from 'src/app/validators/custom-validators';
import { UsernameDuplicateValidator } from 'src/app/validators/username-duplicate.validator';

@Component({
  selector: 'app-agent-account-form',
  templateUrl: './agent-account-form.component.html',
  styleUrls: ['./agent-account-form.component.scss']
})
export class AgentAccountFormComponent implements OnInit {

  @Input() agent$: Observable<Agent>;
  @Input() vendors$: Observable<Vendor[]>;
  @Output() formSubmitEvent = new EventEmitter<Agent>();

  public accountForm: FormGroup;

  constructor(private readonly dupeValidator: UsernameDuplicateValidator) { }

  ngOnInit() {

    this.accountForm = new FormGroup({
      userName: new FormControl('',
        [Validators.required, Validators.minLength(2), Validators.maxLength(100)],
        this.dupeValidator.checkUsername.bind(this.dupeValidator)),
      password: new FormControl('', [CustomValidators.password(6, 20)]),
      confirmationPassword: new FormControl('', [CustomValidators.password(6, 20)]),
      emailAddress: new FormControl('', [Validators.required, Validators.maxLength(100), CustomValidators.emailAddress]),
      firstName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      lastName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      vendor: new FormControl('', Validators.required),
      active: new FormControl(true),
    });

    this.accountForm.get('password').validator = Validators.compose([
      this.accountForm.get('password').validator,
      CustomValidators.equal(this.accountForm.get('confirmationPassword'))
    ]);

    this.accountForm.get('confirmationPassword').validator = Validators.compose([
      this.accountForm.get('confirmationPassword').validator,
      CustomValidators.equal(this.accountForm.get('password'))
    ]);

    // populate form if we have a agent bound to the form
    if (this.agent$) {
      this.agent$.subscribe((result: Agent) => {
        this.accountForm.patchValue(result);
        this.accountForm.patchValue(result.userAccount);
        this.accountForm.patchValue({ vendor: result.vendorId });
      });
    } else {
      // require a password for creating an agent
      this.accountForm.get('password').validator = Validators.compose([
        this.accountForm.get('password').validator, Validators.required
      ]);

      this.accountForm.get('confirmationPassword').validator = Validators.compose([
        this.accountForm.get('confirmationPassword').validator, Validators.required
      ]);
    }


    // only display vendors that are active.
    this.vendors$ = this.vendors$.pipe(map(vendors => vendors.filter(v => v.active)));

  }

  onSubmit() {

    if (!this.accountForm.valid) {
      return;
    }

    const agent = this.buildAgent();

    this.formSubmitEvent.emit(agent);
  }

  private buildAgent(): Agent {

    const agent = new Agent();
    agent.userAccount = new UserAccount();

    agent.userAccount.type = AccountType.Agent;
    agent.userAccount.userName = this.accountForm.controls['userName'].value;
    agent.userAccount.password = this.accountForm.controls['password'].value;
    agent.userAccount.confirmationPassword = this.accountForm.controls['confirmationPassword'].value;
    agent.userAccount.emailAddress = this.accountForm.controls['emailAddress'].value;
    agent.userAccount.active = this.accountForm.controls['active'].value;


    agent.firstName = this.accountForm.controls['firstName'].value;
    agent.lastName = this.accountForm.controls['lastName'].value;
    agent.vendorId = this.accountForm.controls['vendor'].value;

    return agent;
  }

}
