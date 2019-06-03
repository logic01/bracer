import { IntakeFormType } from './enums/intake-form-type.enum';
import { Question } from './question.model';

export class IntakeForm {
    public intakeFormId: string;
    public patientId: string;
    public intakeFormType: IntakeFormType;
    public createdOn: string;
    public modifiedOn: string;
    public questions: Question[];
}
