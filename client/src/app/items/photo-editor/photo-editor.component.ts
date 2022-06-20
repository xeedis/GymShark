import { Component, Input, OnInit } from '@angular/core';
import { Item } from 'src/app/_models/item';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { ItemsService } from 'src/app/_services/items.service';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() item: Item;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User;

  constructor(private accountService: AccountService, private itemService:ItemsService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>{
      this.user = user;
    })
   }

  ngOnInit(): void {
    this.initializeUploader(this.item);
  }

  fileOverBase(e:any){
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader(item: Item){
    this.uploader = new FileUploader({
      url:this.baseUrl+ 'products/'+item.productName+'/add-photo',
      authToken: 'Bearer'+this.user.token,
      isHTML5:true,
      allowedFileType:['image'],
      removeAfterUpload: true,
      autoUpload:false,
      maxFileSize:10*1024*1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers)=>{
      if(response){
        const photo = JSON.parse(response);
        this.item.photos.push(photo);
      }
    }
  }

  setMainPhoto(photo:Photo){
    this.itemService.setMainPhoto(this.item.productName, photo.id).subscribe(()=>{
      this.item.photoUrl = photo.url;
      this.item.photos.forEach(p=>{
        if(p.isMain) p.isMain = false;
        if(p.id === photo.id) p.isMain = true;
      })
    })
  }

  deletePhoto(photoId:number){
    this.itemService.deletePhoto(this.item.productName, photoId).subscribe(()=>{
      this.item.photos = this.item.photos.filter(x=>x.id !== photoId);
    })
  }
}
