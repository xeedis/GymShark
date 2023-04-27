import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Item } from '../_models/item';
import { PaginatedResult } from '../_models/pagination';
import { ItemParams } from '../_models/itemsParams';
import { AccountService } from './account.service';
import { User } from '../_models/user';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {
  baseUrl = environment.apiUrl;
  items: Item[] = [];
  memberCache= new Map();
  user: User;
  itemParams: ItemParams;

  constructor(private http:HttpClient, private accountService:AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>{
      this.user = user;
      this.itemParams = new ItemParams();
    })
   }

   getItemParams(){
    return this.itemParams;
   }

   setItemParams(itemParams: ItemParams){
    this.itemParams = itemParams;
   }

   resetItemParams(){
    this.itemParams = new ItemParams();
    return this.itemParams;
   }

  getItems(itemParams: ItemParams){
    var response = this.memberCache.get(Object.values(itemParams).join('-'));
    if(response){ 
      return of(response);
    }

    let params = getPaginationHeaders(itemParams.pageNumber, itemParams.pageSize);
    params = params.append('category', itemParams.category);
    params = params.append('minPrice', itemParams.minPrice.toString());
    params = params.append('maxPrice', itemParams.maxPrice.toString());


    return getPaginatedResult<Item[]>(this.baseUrl + 'products',params, this.http)
    .pipe(map(response => {
      this.memberCache.set(Object.values(itemParams).join('-'), response)
      return response;
    }))
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

  setMainPhoto(productName:string, photoId:number){
    return this.http.put(this.baseUrl + 'products/'+ productName + '/set-main-photo/'+photoId, {});
  }

  deletePhoto(productName:string, photoId:number){
    return this.http.delete(this.baseUrl + 'products/' + productName + '/delete-photo/' + photoId);
  }

  addLike(productname:string){
    return this.http.post(this.baseUrl +'orders/'+productname,{});
  }

  getLikes(predicate: string, pageNumber: number, pageSize: number){
    let params = getPaginationHeaders(pageNumber, pageSize );

    params = params.append('predicate', predicate);

    return getPaginatedResult<Item[]>(this.baseUrl + 'orders', params, this.http);
  }

}
