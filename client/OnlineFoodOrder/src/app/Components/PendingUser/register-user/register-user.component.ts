import { Component } from '@angular/core';
import { Formvalidators } from '../../../Constant/Formvalidators';
import FormValidationCheck from '../../../Helpers/FormValidationCheck';
import { PendingUserServiceService } from '../../../Services/pending-user-service.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrl: './register-user.component.css'
})
export class RegisterUserComponent {
  registerUserdetails: FormGroup

  constructor(private fb: FormBuilder, private pendingser: PendingUserServiceService, private router: Router, private toast: NgToastService) {
    this.registerUserdetails = this.fb.group({
      UserName: ['', [Validators.required, Validators.maxLength(30), Validators.pattern(Formvalidators.name), this.nospaces]],
      Email: ['', [Validators.required, Validators.pattern(Formvalidators.mail)]],
      UserPhone: ['', [Validators.required, Validators.pattern(Formvalidators.phone)]],
      UserLocation: ['', [Validators.required]]
    })
  }

  get userName() {
    return this.registerUserdetails.get('UserName')
  }
  get userEmail() {
    return this.registerUserdetails.get('Email')
  }
  get userPhonenumber() {
    return this.registerUserdetails.get('UserPhone')
  }
  get userLocation() {
    return this.registerUserdetails.get('UserLocation')
  }

  nospaces(enteredvalue: AbstractControl) {
    let entervalues = enteredvalue.value;
    if (entervalues && (entervalues.startsWith(' ') || entervalues.endsWith(' '))) {
      return { nonamespace: true }
    }
    return null;
  }

  nodigits(enteredvalue: any) {
    let values = enteredvalue.target.value;
    values = values.replace(/[1234567890-=+_!@#$%^&*()~`{}|;:'",.<>?/]/g, '');
    enteredvalue.target.value = values;
  }

  noalphabets(enteredvalue: any) {
    let noalphachars = enteredvalue.target.value;
    noalphachars = noalphachars.replace(/[^+0-9-]/g, '')
    enteredvalue.target.value = noalphachars;
  }

  registerdUserValues() {
    if (this.registerUserdetails.valid) {
      console.log(this.registerUserdetails.value);
      this.pendingser.registerUser(this.registerUserdetails.value).subscribe({
        next: (response) => {
          this.registerUserdetails.reset();
          this.toast.success({ detail: "Registered!please check your mail", summary: response.message, duration: 5000 });
          this.router.navigate(['login']);

        }, error: (err) => {
          const error = err.error;
          if (error) {
            this.toast.error({ detail: 'ERROR', summary: 'Email already exists', duration: 5000 });
          }
          else {
            this.toast.error({ detail: 'ERROR', summary: "Something went wrong", duration: 5000 });
          }
        },
      })
    } else {
      FormValidationCheck.ValidateallformFields(this.registerUserdetails);
    }
  }
}
