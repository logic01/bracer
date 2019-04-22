import { IntakeFormType } from './enums/intake-form-type.enum';
import { Question } from './question.model';

export class IntakeForm {
    intakeFormId: string;
    patientId: string;
    intakeFormType: IntakeFormType;
    createdOn: string;
    modifiedOn: string;
    questions: Question[];
}
