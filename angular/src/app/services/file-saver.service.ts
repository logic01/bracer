import { Injectable } from '@angular/core';
import { Response } from '@angular/http';

import { saveAs } from 'file-saver';

@Injectable({ providedIn: 'root' })
export class FileSaverService {

    saveFile(blobContent: Blob, fileName: string) {
        const blob = new Blob([blobContent], { type: 'application/octet-stream' });
        saveAs(blob, fileName);
    }

    getFileNameFromResponseContentDisposition(res: Response) {
        const contentDisposition = res.headers.get('content-disposition') || '';
        const matches = /filename=([^;]+)/ig.exec(contentDisposition);
        const fileName = (matches[1] || 'untitled').trim();
        return fileName;
    }
}
