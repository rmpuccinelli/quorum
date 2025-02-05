import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

import { BillAnalysis } from '../models/BillAnalysis';
import { LegislatorAnalysis } from '../models/LegislatorAnalysis';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class QuorumService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl;

  getBillAnalysis(): Observable<BillAnalysis[]> {
    return this.http.get<BillAnalysis[]>(`${this.apiUrl}/LegislativeAnalysis/bill/support-analysis`)
      .pipe(
        catchError((error) => this.handleError(error))
      );
  }

  getLegislatorAnalysis(): Observable<LegislatorAnalysis[]> {
    return this.http.get<LegislatorAnalysis[]>(`${this.apiUrl}/LegislativeAnalysis/legislator/voting-records`)
      .pipe(
        catchError((error) => this.handleError(error))
      );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage: string;

    if (typeof error.error === 'string') {
      errorMessage = error.error;
    } else if (error.error?.message) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    return throwError(() => new Error(errorMessage));
  }
}
