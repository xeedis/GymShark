import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { purharseModel } from '../_models/purharseModel';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createPurharse(purharseModel: purharseModel){
    return this.http.post(this.baseUrl + 'opinion/', {purharseModel})
  }
}
