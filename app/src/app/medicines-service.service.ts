import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MedicinesResponse } from '../Models/GetMedicinesResponse';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MedicinesServiceService {

  private medicinesURL = "https://192.168.0.126:45455/";

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

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      console.error(error); 
  
      return of(result as T);
    };
  }
}
