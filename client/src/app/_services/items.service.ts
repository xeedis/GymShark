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

    let params = this.getPaginationHeaders(itemParams.pageNumber, itemParams.pageSize);
    params = params.append('category', itemParams.category);
    params = params.append('minPrice', itemParams.minPrice.toString());
    params = params.append('maxPrice', itemParams.maxPrice.toString());


    return this.getPaginatedResult<Item[]>(this.baseUrl + 'products',params)
    .pipe(map(response => {
      this.memberCache.set(Object.values(itemParams).join('-'), response)
      return response;
    }))
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number){
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return params;
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

  getLikes(predicate: string){
    return this.http.get(this.baseUrl + 'orders?=' + predicate);
  }

  private getPaginatedResult<T>(url, params) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          return paginatedResult;
        }
      })
    );
  }
}
