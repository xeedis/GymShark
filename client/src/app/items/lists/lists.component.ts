import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/_models/item';
import { Pagination } from 'src/app/_models/pagination';
import { ItemsService } from 'src/app/_services/items.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  items: Item[] | undefined;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination | undefined;
  totalCost = 0;

  constructor(private itemService: ItemsService) { }

  ngOnInit(): void {
    this.loadOrders();
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
   
  pageChanged(event: any){
    if(this.pageNumber !== event.page)
    {
      this.pageNumber = event.page;
      this.loadOrders();
    }
  }

}
