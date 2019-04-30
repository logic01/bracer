import { DocumentStatus } from './enums/document-status.enum';
import { DocumentType } from './enums/document-type.enum';

export class Document {
    documentId: string;
    intakeFormId: string;
    physicianId: string;
    type: DocumentType;
    status: DocumentStatus;
    content: string;
    signature: string;
}
