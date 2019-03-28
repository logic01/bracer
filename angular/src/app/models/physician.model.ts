import { Address } from './address.model';
import { UserAccount } from './user-account.model';

export class Physician {
    userAccount: UserAccount;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    address: Address;
}
