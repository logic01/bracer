import { Address } from './address.model';
import { ErrorModel } from './error.model';

export class Vendor {
    public companyName: string;
    public doingBusinessAs: string;
    public billingAddress: Address;
    public phoneNumber: string;
    public contactFirstName: string;
    public contactLastName: string;
    public error: ErrorModel;
}
