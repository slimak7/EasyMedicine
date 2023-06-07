import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Medicine } from '../Models/Medicine';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MedicinesServiceService {

  private medicinesURL = "https://192.168.0.126:45455/";

  constructor(private http: HttpClient) { }

  public getMedicinesByRange(index: number, count: number): Observable<Medicine[]> {
    return this.http.get<Medicine[]>(`${this.medicinesURL}Medicines/GetMedicines/GetByRange/${index}/${count}`)
    .pipe(
      catchError(this.handleError<Medicine[]>('getMedicines', []))
    );
  }

  public getMedicinesByName(name: string): Observable<Medicine[]> {
    return this.http.get<Medicine[]>(`${this.medicinesURL}Medicines/GetMedicines/GetByName/${name}`)
    .pipe(
      catchError(this.handleError<Medicine[]>('getMedicines', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      console.error(error); 
  
      return of(result as T);
    };
  }
}
