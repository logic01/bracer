import { UserAccount } from './user-account.model';
import { Address } from './address.model';

export class Physician {
    userAccount: UserAccount;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    contactFirstName: string;
    contactLastName: string;
    address: Address;
}
