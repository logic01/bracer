import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Signature } from 'src/app/models/signature.model';

import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class SignatureService {

    constructor(private http: HttpClient) { }
    sign(intakeFormId: string, signature: Signature): Observable<void> {
        return this.http.post<void>(`${environment.api_url}/intakeform/${intakeFormId}/signature`, signature);
    }
}
