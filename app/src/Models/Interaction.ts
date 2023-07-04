import { Substance } from 'src/Models/Substance';

export interface Interaction {
substance: Substance;
interactionName: string;
interactionDescription: string;
}