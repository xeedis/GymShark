import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Item } from 'src/app/_models/item';
import { ItemsService } from 'src/app/_services/items.service';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  item: Item;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  constructor(private itemService:ItemsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadItem();

    this.galleryOptions =[
      {
        width:'500px',
        height:'500px',
        imagePercent:100,
        thumbnailsColumns: 4,
        imageAnimation:NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
  }


  getImages():NgxGalleryImage[]{
    const imageUrls = [];
    for(const photo of this.item.photos){
      imageUrls.push({
        small:photo?.url,
        medium: photo?.url,
        big:photo?.url
      })
    }
    return imageUrls;
  }

  loadItem(){
  this.itemService.getItem(this.route.snapshot.paramMap.get('productname')).subscribe(item=>{
    this.item = item;
    this.galleryImages = this.getImages();
  })
  }
}
