import { Medicine } from 'src/Models/Medicine';

export interface MedicinesResponse {
    errors: null;
    medicines: Medicine[];
    success: boolean;
}