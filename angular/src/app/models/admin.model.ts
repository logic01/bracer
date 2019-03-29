import { ErrorModel } from './error.model';
import { UserAccount } from './user-account.model';

export class Admin {
    firstName: string;
    lastName: string;
    userAccount: UserAccount;
    error: ErrorModel;
}
