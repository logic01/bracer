import { IntakeFormType } from './enums/intake-form-type.enum';
import { IntakeStatus } from './enums/intake-status.enum';
import { Question } from './question.model';

export class IntakeForm {
    public intakeFormId: string;
    public patientId: string;
    public physicianId: string;
    public intakeFormType: IntakeFormType;
    public ICD10: string;
    public HCPCS: string;
    public status: IntakeStatus;
    public createdOn: string;
    public modifiedOn: string;
    public questions: Question[];
}
