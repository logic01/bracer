import { AccountType } from './Enums/account.type.enum';
import { ErrorModel } from './error.model';

export class UserAccount {
    userId: string;
    type: AccountType;
    userName: string;
    password: string;
    active: boolean;
    confirmationPassword: string;
    errors: ErrorModel[];
}
