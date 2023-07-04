import { Component, OnInit } from '@angular/core';
import { Observable, Subject, map } from 'rxjs';
import { Medicine } from 'src/Models/Medicine';
import { Interaction } from 'src/Models/Interaction';
import { Injectable } from '@angular/core';
import { CommonModule } from '@angular/common';
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
  imports: [MatButtonModule, MatIconModule, MatDividerModule, FormsModule, MatTableModule, CommonModule]
})
export class MedicinesListComponent  {

  
  interactions: Interaction[] = [];

  foundedMedicines: Medicine[] = [];
  medicinesColumnsToDisplay = ['ID', 'Name', 'Power', 'Company', 'Substances'];
  interactionsColumnsToDisplay = ['OriginalName', 'TranslatedName', 'InteractionLevel', 'InteractionDescription'];
  medicineName: string = "";


  constructor(private medicinesService: MedicinesServiceService) {}

  searchByName(): void {

    this.medicinesService.getMedicinesByName(this.medicineName).subscribe(medicines => { this.foundedMedicines = medicines.medicines; console.log(this.foundedMedicines); });
 
    
  }

  getInteractions(id: string): void {
    this.medicinesService.getMedicinesInteractions(id).subscribe(interactions => { this.interactions = interactions.interactions; console.log(this.interactions); });
  }
}
