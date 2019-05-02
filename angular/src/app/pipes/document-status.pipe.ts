import { Pipe, PipeTransform } from '@angular/core';

import { DocumentStatus } from '../models/enums/document-status.enum';

@Pipe({
  name: 'documentStatus'
})
export class DocumentStatusPipe implements PipeTransform {

  transform(value: DocumentStatus, args?: any): any {
    switch (value) {
      case DocumentStatus.New: {
        return 'New';
      }
      case DocumentStatus.Approved: {
        return 'Approved';
      }
      case DocumentStatus.Assigned: {
        return 'Assigned';
      }
      case DocumentStatus.Signed: {
        return 'Signed';
      }
      case DocumentStatus.Closed: {
        return 'Closed';
      }
      default: {
        return 'New';
      }
    }
  }

}
