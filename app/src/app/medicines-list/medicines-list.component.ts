import { Component, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Medicine } from 'src/Models/Medicine';
import { Injectable } from '@angular/core';
import { MedicinesServiceService } from '../medicines-service.service'
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import { FormsModule } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-medicines-list',
  templateUrl: './medicines-list.component.html',
  styleUrls: ['./medicines-list.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatIconModule, MatDividerModule, FormsModule]
})
export class MedicinesListComponent {

  medicines$!: Observable<Medicine[]>;
  medicineName: string = "";

  constructor(private medicinesService: MedicinesServiceService) {}

  searchByName(): void {

    this.medicines$ = this.medicinesService.getMedicinesByName(this.medicineName);
    
    this.medicines$.subscribe(val => console.log(val));
  }
}
