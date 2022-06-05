import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from '../_models/item';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {
  baseUrl = environment.apiUrl;

  constructor(private http:HttpClient) { }

  getItems(){
    return this.http.get<Item[]>(this.baseUrl + 'products');
  }

  getItem(productName:string){
    return this.http.get<Item>(this.baseUrl +'products/'+ productName);
  }
}
