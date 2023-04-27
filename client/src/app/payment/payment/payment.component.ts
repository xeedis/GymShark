import { Component, Input, OnInit } from '@angular/core';
import { Item } from 'src/app/_models/item';
import { Pagination } from 'src/app/_models/pagination';
import { purharseModel } from 'src/app/_models/purharseModel';
import { ItemsService } from 'src/app/_services/items.service';
import { PaymentService } from 'src/app/_services/payment.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  items: Item[] | undefined;
  purharseModel: purharseModel;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination | undefined;
  totalCost = 0;
  constructor(private itemService:ItemsService, private paymentService:PaymentService) { }

  ngOnInit(): void {
    this.loadOrders()
    console.log(this.items);
  }

  loadOrders()
  {
    this.itemService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe({
      next: response =>{
        this.items = response.result;
        this.pagination = response.pagination; 
        this.items.forEach(item=> this.totalCost+= item.price);
      }
    })  
  }

  purharseItems(){
    
  }
}
