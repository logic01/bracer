import { Address } from './address.model';
import { CallbackTime } from './enums/callback-time.enum';
import { InsuranceType } from './enums/insurance-type.enum';
import { LanguageType } from './enums/language-type.enum';
import { PharmacyType } from './enums/pharmacy-type.enum';
import { SexType } from './enums/sex-type.enum';
import { TherapyType } from './enums/therapy-type.enum';
import { ErrorModel } from './error.model';

export class Patient {
    patientId: string;
    firstName: string;
    middleName: string;
    lastName: string;
    phoneNumber: string;
    dateOfBirth: Date;
    callBackImmediately: boolean;
    bestTimeToCallBack: CallbackTime;
    language: LanguageType;
    sex: SexType;
    address: Address;

    medications: string;
    notes: string;
    otherProducts: string;
    physiciansName: string;
    physiciansPhoneNumber: string;
    physiciansAddress: Address;
    therapy: TherapyType;
    insurance: InsuranceType;
    pharmacy: PharmacyType;
    isDme: boolean;
    error: ErrorModel;
}
