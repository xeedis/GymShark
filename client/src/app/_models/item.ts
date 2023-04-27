import { Photo } from "./photo";

export interface Item {
    id:string;
    productName: string;
    description: string;
    aboutCompany: string;
    specifiedFor: string;
    photoUrl: string;
    price: number;
    category: string;
    photos: Photo[];
}

