import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgToastService } from 'ng-angular-popup';
import { Formvalidators } from '../../../Constant/Formvalidators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-gateway',
  templateUrl: './payment-gateway.component.html',
  styleUrl: './payment-gateway.component.css'
})
export class PaymentGatewayComponent implements OnInit {
  paymentForm!: FormGroup;
  bankList: any[] = [
    { name: 'SBI', url: 'https://retail.onlinesbi.sbi/retail/login.htm' },
    { name: 'IOB', url: 'https://www.iobnet.co.in/ibanking/login.do' },
  ];

  constructor(private fb: FormBuilder, private toast: NgToastService, private router: Router) { }

  ngOnInit() {
    this.paymentForm = this.fb.group({
      paymentOption: ['', Validators.required],
      selectedBank: [''],
      upiId: ['', [Validators.required, Validators.pattern(Formvalidators.upiId)]],
      cardNumber: ['', [Validators.required, Validators.pattern(Formvalidators.cardNumber)]],
      expiryDate: ['', [Validators.required, Validators.pattern(Formvalidators.expiryDate)]],
      cvv: ['', [Validators.required, Validators.pattern(Formvalidators.cvv)]]
    });
  }

  get option() {
    return this.paymentForm.get('paymentOption');
  }

  get card() {
    return this.paymentForm.get('cardNumber');
  }
  get expdate() {
    return this.paymentForm.get('expiryDate');
  }
  get cvv() {
    return this.paymentForm.get('cvv');
  }

  get bank() {
    return this.paymentForm.get('selectedBank');
  }

  get upi() {
    return this.paymentForm.get('upiId');
  }


  processPayment() {
    if (this.option?.value === 'COD') {
      this.toast.success({ detail: "Order Added", summary: "Wait for approval", duration: 3000 });
      console.log('Cash on delivery applied');
      this.router.navigate(['/admin-dashboard/user-order'])
    } else if (this.paymentForm.valid) {
      if (this.option?.value === 'onlineBanking' && this.option.value) {
        window.open(this.bank?.value, '_blank');
      } else if (this.option?.value === 'upi') {
        this.toast.success({ detail: "Order Added", summary: "Wait for approval", duration: 3000 });
        console.log('payment with UPI ID:', this.upi?.value);
      } else if (this.option?.value === 'card') {
        this.toast.success({ detail: "Order Added", summary: "Wait for approval", duration: 3000 });
        console.log('payment with card number:', this.card?.value);
      } else {
        console.log('Processing payment...');
      }
    } else {
      this.toast.error({ detail: 'ERROR', summary: 'Please Make a Payment!', duration: 3000 });
    }
  }
}
