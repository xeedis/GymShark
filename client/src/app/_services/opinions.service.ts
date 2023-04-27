import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Opinion } from '../_models/opinion';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class OpinionsService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOpinions(pageNumber: number, pageSize:number, container: string, productName: string){
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<Opinion[]>(this.baseUrl + 'opinion/'+ productName, params, this.http);
  }

  createOpinion(recipientName: string,content:string){
    return this.http.post(this.baseUrl +'opinion/',{recipientName,content});
  }
}
