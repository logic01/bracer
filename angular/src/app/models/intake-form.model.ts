import { IntakeFormType } from './enums/intake-form-type.enum';
import { IntakeStatus } from './enums/intake-status.enum';
import { ICD10Code } from './icd10-code.model';
import { Question } from './question.model';

export class IntakeForm {
    public intakeFormId: string;
    public patientId: string;
    public physicianId: string;
    public documentId: string;
    public status: IntakeStatus;
    public intakeFormType: IntakeFormType;
    public questions: Question[];
    public ICD10Codes: ICD10Code[];
    public HCPCSCode: string;
    public duration: string;
    public physicianNotes: string;
    public deniedReason: string;
    public product: string;
    public vendorBilled: boolean;
    public vendorPaid: boolean;
    public physicianPaid: boolean;
    public createdOn: string;
    public modifiedOn: string;
}
