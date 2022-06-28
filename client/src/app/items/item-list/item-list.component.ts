import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/_models/item';
import { Pagination } from 'src/app/_models/pagination';
import { ItemParams } from 'src/app/_models/itemsParams';
import { ItemsService } from 'src/app/_services/items.service';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_models/user';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {
  items: Item[];
  pagination: Pagination;
  itemParams: ItemParams;
  user:User;
  categoryList = [{value:'all', display:'All'},{value:'supplement', display: 'Supplies'},{value:'tool', display:'Tools'},{value:'wear', display:'Wears'}]

  constructor(private itemService: ItemsService) { 
    this.itemParams = this.itemService.getItemParams()
  }

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(){
    this.itemService.setItemParams(this.itemParams);
    this.itemService.getItems(this.itemParams).subscribe(response=>{
      this.items = response.result;
      this.pagination = response.pagination;
    })
  }

  resetFilters(){
    this.itemParams = this.itemService.resetItemParams();
    this.loadItems();
  }

  pageChanged(event: any){
    this.itemParams.pageNumber = event.page;
    this.itemService.setItemParams(this.itemParams);
    this.loadItems();
  }

}
