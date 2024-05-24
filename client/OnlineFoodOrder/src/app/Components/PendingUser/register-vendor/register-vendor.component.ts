import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Formvalidators } from '../../../Constant/Formvalidators';
import FormValidationCheck from '../../../Helpers/FormValidationCheck';
import { PendingVendorServiceService } from '../../../Services/pending-vendor-service.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-register-vendor',
  templateUrl: './register-vendor.component.html',
  styleUrl: './register-vendor.component.css'
})
export class RegisterVendorComponent {
  registerVendordetails: FormGroup

  constructor(private fb: FormBuilder, private vendorser: PendingVendorServiceService, private router: Router, private toast: NgToastService) {
    this.registerVendordetails = this.fb.group({
      UserName: ['', [Validators.required, Validators.maxLength(30), Validators.pattern(Formvalidators.name), this.nospaces]],
      Email: ['', [Validators.required, Validators.pattern(Formvalidators.mail)]],
      UserPhone: ['', [Validators.required, Validators.pattern(Formvalidators.phone)]],
      UserLocation: ['', [Validators.required]]
    })
  }

  get vendorName() {
    return this.registerVendordetails.get('UserName')
  }
  get vendorEmail() {
    return this.registerVendordetails.get('Email')
  }
  get vendorPhonenumber() {
    return this.registerVendordetails.get('UserPhone')
  }
  get vendorLocation() {
    return this.registerVendordetails.get('UserLocation')
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

  registerdVendorValues() {
    if (this.registerVendordetails.valid) {
      console.log(this.registerVendordetails.value);
      this.vendorser.registerVendor(this.registerVendordetails.value).subscribe({
        next: (response) => {
          this.registerVendordetails.reset();
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
        }
      })

    } else {
      alert("please fill all fields");
      FormValidationCheck.ValidateallformFields(this.registerVendordetails);
    }
  }

}
