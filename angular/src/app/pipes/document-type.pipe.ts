import { Pipe, PipeTransform } from '@angular/core';

import { DocumentType } from '../models/enums/document-type.enum';

@Pipe({
  name: 'documentType'
})
export class DocumentTypePipe implements PipeTransform {

  transform(value: DocumentType, args?: any): any {
    switch (value) {
      case DocumentType.IntakeForm: {
        return 'IntakeForm';
      }
      case DocumentType.PatientForm: {
        return 'PatientForm';
      }
      default: {
        return 'IntakeForm';
      }
    }
  }

}
