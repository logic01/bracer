import { ErrorModel } from './error.model';

export class UserAccount {
    userId: string;
    userName: string;
    password: string;
    confirmationPassword: string;
    error: ErrorModel;
}
