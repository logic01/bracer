import { DocumentStatus } from './enums/document-status.enum';
import { DocumentType } from './enums/document-type.enum';

export class Document {
    public documentId: string;
    public intakeFormId: string;
    public physicianId: string;
    public type: DocumentType;
    public status: DocumentStatus;
    public content: string;
    public signature: string;
    public createdOn: Date;
    public modifiedOn: Date;
}
