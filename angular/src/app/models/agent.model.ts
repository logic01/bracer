import { ErrorModel } from './error.model';
import { UserAccount } from './user-account.model';

export class Agent {
    firstName: string;
    lastName: string;
    vendorId: number;
    userAccount: UserAccount;
    error: ErrorModel;
}
