import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Item } from 'src/app/_models/item';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { ItemsService } from 'src/app/_services/items.service';

@Component({
  selector: 'app-item-edit',
  templateUrl: './item-edit.component.html',
  styleUrls: ['./item-edit.component.css']
})
export class ItemEditComponent implements OnInit {
  @ViewChild('editForm') editForm:NgForm;
  user: User;
  item: Item;
  @HostListener('window:beforeunload',['$event']) unloadNotification($event:any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }
  constructor(private accountService: AccountService, private itemService: ItemsService,private route: ActivatedRoute,
    private toastr:ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadItem();
  }

  loadItem(){
    this.itemService.getItem(this.route.snapshot.paramMap.get('productname')).subscribe(item=>{
      this.item = item;
    })
  }

  updateItem(){
    this.itemService.updateItem(this.item, this.route.snapshot.paramMap.get('productname')).subscribe(()=>{
      this.toastr.success('Data updated successfuly');
      this.editForm.reset(this.item);
    });
    
  }
}
