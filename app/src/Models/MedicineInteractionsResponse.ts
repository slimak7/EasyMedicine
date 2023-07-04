import { Interaction } from 'src/Models/Interaction';

export interface MedicineInteractionsResponse {

    errors: null;
    success: boolean;
    interactions: Interaction[]
}