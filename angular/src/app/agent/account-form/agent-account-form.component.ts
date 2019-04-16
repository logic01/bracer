import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { Agent } from 'src/app/models/Agent.model';
import { UserAccount } from 'src/app/models/user-account.model';
import { Observable } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Vendor } from 'src/app/models/vendor.model';
import { CustomValidators } from 'src/app/validators/custom-validators';

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

  constructor() { }

  ngOnInit() {
    this.accountForm = new FormGroup({
      userName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      password: new FormControl('', CustomValidators.password(6, 20)),
      confirmationPassword: new FormControl('', CustomValidators.password(6, 20)),
      firstName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      lastName: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      vendor: new FormControl('', Validators.required)
    });

    this.accountForm.get('password').validator = Validators.compose([
      this.accountForm.get('password').validator,
      CustomValidators.equal(this.accountForm.get('confirmationPassword'))
    ]);

    this.accountForm.get('confirmationPassword').validator = Validators.compose([
      this.accountForm.get('confirmationPassword').validator,
      CustomValidators.equal(this.accountForm.get('password'))
    ]);

    // populate form if we have a vendor bound to the form
    if (this.agent$) {
      this.agent$.subscribe((result: Agent) => {
        this.accountForm.patchValue(result);
        this.accountForm.patchValue(result.userAccount);
        this.accountForm.patchValue({ vendor: result.vendorId });
      });
    }

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

    agent.userAccount.userName = this.accountForm.controls['userName'].value;
    agent.userAccount.password = this.accountForm.controls['password'].value;
    agent.userAccount.confirmationPassword = this.accountForm.controls['confirmationPassword'].value;
    agent.firstName = this.accountForm.controls['firstName'].value;
    agent.lastName = this.accountForm.controls['lastName'].value;
    agent.vendorId = this.accountForm.controls['vendor'].value;
    return agent;
  }

}
