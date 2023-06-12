import { Component, OnInit } from '@angular/core';
import { Observable, Subject, map } from 'rxjs';
import { Medicine } from 'src/Models/Medicine';
import { Injectable } from '@angular/core';
import { MedicinesServiceService } from '../medicines-service.service'
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import { FormsModule } from '@angular/forms';
import {MatTableModule} from '@angular/material/table';
import { __values } from 'tslib';

@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-medicines-list',
  templateUrl: './medicines-list.component.html',
  styleUrls: ['./medicines-list.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatIconModule, MatDividerModule, FormsModule, MatTableModule]
})
export class MedicinesListComponent  {

  medicines$!: Observable<Medicine[]>;
  foundedMedicines: Medicine[] = [];
  columnsToDisplay = ['ID', 'Name', 'Power', 'Company', 'Substances'];
  medicineName: string = "";


  constructor(private medicinesService: MedicinesServiceService) {}

  searchByName(): void {

    this.medicinesService.getMedicinesByName(this.medicineName).subscribe(medicines => { this.foundedMedicines = medicines.medicines; console.log(this.foundedMedicines); });
 
    
  }
}
