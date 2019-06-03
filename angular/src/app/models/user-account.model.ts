import { AccountType } from './enums/account-type.enum';
import { ErrorModel } from './error.model';

export class UserAccount {
    public userAccountId: string;
    public type: AccountType;
    public userName: string;
    public password: string;
    public emailAddress: string;
    public active: boolean;
    public confirmationPassword: string;
    public errors: ErrorModel[];
}
