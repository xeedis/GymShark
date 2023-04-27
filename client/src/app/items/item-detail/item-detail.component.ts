import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { Opinion } from 'src/app/_models/opinion';
import { Pagination } from 'src/app/_models/pagination';
import { ItemsService } from 'src/app/_services/items.service';
import { OpinionsService } from 'src/app/_services/opinions.service';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  item: Item;
  opinions?: Opinion[];
  content: string;
  pagination?: Pagination;
  container = "Unread";
  pageNumber = 1;
  pageSize = 5;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  productName = this.route.snapshot.paramMap.get('productname');
  canComment = false;
  constructor(private itemService:ItemsService,private opinionService:OpinionsService,private route: ActivatedRoute, private toastr: ToastrService) { }

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
    this.loadOpinions();
  })
  }

  loadOpinions() {
    this.opinionService.getOpinions(this.pageNumber, this.pageSize, this.container, this.productName).subscribe({
      next: response => {
        this.opinions = response.result;
        this.pagination = response.pagination;
      }
    })
  }
  
  addLike(item: Item){
    this.itemService.addLike(item.productName).subscribe(()=>{
      this.toastr.success('You have ordered ' + item.productName);
    })
  }

  pageChanged(event:any){
    if(this.pageNumber != event.page){
      this.pageNumber = event.page;
      this.loadOpinions();
    }
  }

  addOpinion(item: Item){
    this.opinionService.createOpinion(item.productName, this.content).subscribe(()=>{
    this.toastr.success("Your opinion has been sent")
  })
  }
  onClick(){
    this.canComment = !this.canComment;
    console.log(this.content)
  }
}
