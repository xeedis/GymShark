import { Photo } from "./photo";

export interface Item {
    id:string;
    productName: string;
    description: string;
    photoUrl: string;
    price: number;
    category: string;
    photos: Photo[];
}

