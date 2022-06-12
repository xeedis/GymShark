import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Item } from '../_models/item';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {
  baseUrl = environment.apiUrl;
  items: Item[] = [];

  constructor(private http:HttpClient) { }

  getItems(){
    if(this.items.length > 0) return of(this.items);
    return this.http.get<Item[]>(this.baseUrl + 'products').pipe(
      map(items=>{
        this.items = items;
        return items;
      })
    );
  }

  getItem(productName:string){
    const item = this.items.find(x=>x.productName == productName);
    if(item !== undefined) return of(item);
    return this.http.get<Item>(this.baseUrl +'products/'+ productName);
  }

  updateItem(item: Item, productName:string){
    return this.http.put(this.baseUrl + 'products/' + productName, item).pipe(
      map(()=>{
        const index = this.items.indexOf(item);
        this.items[index] = item;
      })
    );
  }
}
