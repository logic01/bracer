import { Address } from './address.model';
import { CallbackTime } from './enums/callback-time.enum';
import { InsuranceType } from './enums/insurance-type.enum';
import { LanguageType } from './enums/language-type.enum';
import { PharmacyType } from './enums/pharmacy-type.enum';
import { SexType } from './enums/sex-type.enum';
import { TherapyType } from './enums/therapy-type.enum';

export class Patient {
    public patientId: string;
    public firstName: string;
    public middleName: string;
    public lastName: string;
    public phoneNumber: string;
    public dateOfBirth: Date;
    public callBackImmediately: boolean;
    public bestTimeToCallBack: CallbackTime;
    public language: LanguageType;
    public sex: SexType;
    public address: Address;

    public medications: string;
    public notes: string;
    public otherProducts: string;
    public physiciansName: string;
    public physiciansPhoneNumber: string;
    public physiciansAddress: Address;
    public therapy: TherapyType;
    public insurance: InsuranceType;
    public pharmacy: PharmacyType;
    public isDme: boolean;
    public createdOn: Date;
    public modifiedOn: Date;
}
