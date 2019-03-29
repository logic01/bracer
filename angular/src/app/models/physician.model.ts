import { Address } from './address.model';
import { ErrorModel } from './error.model';
import { UserAccount } from './user-account.model';

export class Physician {
    firstName: string;
    lastName: string;
    phoneNumber: string;
    address: Address;
    userAccount: UserAccount;
    error: ErrorModel;
}
