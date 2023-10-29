import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MedicinesResponse } from '../Models/GetMedicinesResponse';
import { MedicineInteractionsResponse } from 'src/Models/MedicineInteractionsResponse';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MedicinesServiceService {

  private medicinesURL = "http://192.168.0.231:45456/";

  constructor(private http: HttpClient) { }

  public getMedicinesByRange(index: number, count: number): Observable<MedicinesResponse> {
    return this.http.get<MedicinesResponse>(`${this.medicinesURL}Medicines/GetMedicines/GetByRange/${index}/${count}`)
    .pipe(
      catchError(this.handleError<MedicinesResponse>('getMedicines', ))
    );
  }

  public getMedicinesByName(name: string): Observable<MedicinesResponse> {
    return this.http.get<MedicinesResponse>(`${this.medicinesURL}Medicines/GetMedicines/GetByName/${name}`)
    .pipe(
      catchError(this.handleError<MedicinesResponse>('getMedicines', ))
    );
  }

  public getMedicinesBySubstance(substanceID: string): Observable<MedicinesResponse> {
    return this.http.get<MedicinesResponse>(`${this.medicinesURL}Medicines/GetMedicines/GetBySubstance/${substanceID}`)
    .pipe(
      catchError(this.handleError<MedicinesResponse>('getMedicines', ))
    );
  }

  public getMedicinesInteractions(id: string): Observable<MedicineInteractionsResponse> {
    return this.http.get<MedicineInteractionsResponse>(`${this.medicinesURL}Medicines/GetMedicines/GetInteractions/${id}`)
    .pipe(
      catchError(this.handleError<MedicineInteractionsResponse>('getMedicines', ))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      console.error(error); 
  
      return of(result as T);
    };
  }
}
