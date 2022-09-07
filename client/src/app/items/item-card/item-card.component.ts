import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { ItemsService } from 'src/app/_services/items.service';

@Component({
  selector: 'app-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.css']
})
export class ItemCardComponent implements OnInit {
  @Input() item: Item;
  constructor(private itemService:ItemsService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  addLike(item: Item){
    this.itemService.addLike(item.productName).subscribe(()=>{
      this.toastr.success('You have ordered' + item.productName);
    })
  }

}
