import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styles: [
  ]
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  constructor(private accountService:AccountService,private toastr:ToastrService, private fb: FormBuilder,
    private router:Router) { }
  registerMode=false;
  registerForm: FormGroup;
  validationErrors: string[] = [];

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, 
        Validators.minLength(5), Validators.maxLength(25)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })
  }

  matchValues(matchTo:string): ValidatorFn{
    return (control:AbstractControl)=>{
      return control?.value === control?.parent?.controls[matchTo].value ? null : {isMatching: true};
    }
  }

  registerToggle(){
    this.registerMode=!this.registerMode;
  }
  register(){
    this.accountService.register(this.registerForm.value).subscribe(response=>{
      this.router.navigateByUrl('/products');
      this.cancel();
    }, error =>{
      this.validationErrors = error;
    }
    )
  }
  cancel(){
    console.log(this.cancelRegister.emit(false));
  }
}
