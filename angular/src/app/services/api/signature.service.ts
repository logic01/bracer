import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Signature } from 'src/app/models/signature.model';

@Injectable({
    providedIn: 'root'
})
export class SignatureService {

    constructor(private http: HttpClient) { }
    put(intakeFormId: string, signature: Signature): Observable<void> {
        return this.http.post<void>(`${environment.api_url}/intakeform/${intakeFormId}/signature`, signature);
    }
}
