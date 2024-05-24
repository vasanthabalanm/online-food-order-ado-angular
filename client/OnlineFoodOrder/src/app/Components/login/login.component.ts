import { Component } from '@angular/core';
import { Formvalidators } from '../../Constant/Formvalidators';
import FormValidationCheck from '../../Helpers/FormValidationCheck';
import { AdminServiceService } from '../../Services/admin-service.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  logindetails:FormGroup;

  constructor(private fb:FormBuilder,private adminService:AdminServiceService,private router:Router,private toast:NgToastService){
    this.logindetails = this.fb.group({
      Email:['',[Validators.required,Validators.pattern(Formvalidators.mail)]],
      UserPassword:['',[Validators.required]]
    })
  }

  get email(){
    return this.logindetails.get('Email');
  }
  get password(){
    return this.logindetails.get('UserPassword');
  }

  formsubmit(){
    if(this.logindetails.valid){
      this.adminService.login(this.logindetails.value).subscribe({
        next:(response)=>{
          this.adminService.setStorageValues(response.id,response.email,response.role,response.oldPassword);
          this.toast.success({detail:"SUCCESS", summary:response.message, duration: 5000});
          let enteredpass = this.logindetails.get('UserPassword')?.value;
          let fetchedvalue = this.adminService.getOldPass();
          console.log(fetchedvalue);
          if(enteredpass === this.adminService.getOldPass()){
            this.router.navigate(['updatePass'])
          }
          else{
            this.router.navigate(['admin-dashboard'])
          }
        },
        error: (err) =>{
          this.toast.error({detail:"ERROR", summary:"Something when wrong!", duration: 5000});
        },
      });
    }else {
      FormValidationCheck.ValidateallformFields(this.logindetails);
    }
  }
}
