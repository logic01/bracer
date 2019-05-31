import { AccountType } from './enums/account-type.enum';
import { ErrorModel } from './error.model';

export class UserAccount {
    userAccountId: string;
    type: AccountType;
    userName: string;
    password: string;
    emailAddress: string;
    active: boolean;
    confirmationPassword: string;
    errors: ErrorModel[];
}
