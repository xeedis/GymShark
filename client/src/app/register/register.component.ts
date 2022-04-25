import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styles: [
  ]
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  constructor(private accountService:AccountService) { }
  registerMode=false;
  model:any={};
  ngOnInit(): void {
  }
  registerToggle(){
    this.registerMode=!this.registerMode;
  }
  register(){
    this.accountService.register(this.model).subscribe(response=>{
      console.log(response);
      this.cancel();
    }, error =>{
      console.log(error);
    }
    )
  }
  cancel(){
    console.log(this.cancelRegister.emit(false));
  }
}
