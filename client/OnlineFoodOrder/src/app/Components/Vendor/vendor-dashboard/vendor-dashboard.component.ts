import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddBranchServiceService } from '../../../Services/add-branch-service.service';
import { AddHotelService } from '../../../Services/add-hotel.service';
import { NgToastService } from 'ng-angular-popup';
import { Formvalidators } from '../../../Constant/Formvalidators';

@Component({
  selector: 'app-vendor-dashboard',
  templateUrl: './vendor-dashboard.component.html',
  styleUrl: './vendor-dashboard.component.css'
})
export class VendorDashboardComponent implements OnInit {
  addBranchForm: FormGroup
  id!: number;
  listofHotel: any = [];
  constructor(private fb: FormBuilder, private addbranchser: AddBranchServiceService, private addhotelser: AddHotelService, private toast: NgToastService) {
    this.addBranchForm = this.fb.group({
      HotelId: ['', Validators.required],
      BranchName: ['', Validators.required],
      BranchLocation: ['', Validators.required],
      BranchPhoneNumber: ['', [Validators.required, Validators.pattern(Formvalidators.phone)]]
    });
  }

  get hotelid() {
    return this.addBranchForm.get('HotelId');
  }
  get branchname() {
    return this.addBranchForm.get('BranchName');
  }
  get branchlocation() {
    return this.addBranchForm.get('BranchLocation');
  }
  get branchphonenumber() {
    return this.addBranchForm.get('BranchPhoneNumber');
  }

  ngOnInit() {
    this.getAllhotel();
  }

  // addd branch
  branchDetails() {
    this.addbranchser.AddBrach(this.addBranchForm.value).subscribe({
      next: (response) => {
        this.addBranchForm.reset();
        this.toast.success({ detail: "Branch Added", summary: response.message, duration: 5000 });
      },
      error: (err) => {
        let errorMessage = "Something gone wrong!"
        if (err && err.error && err.error.error) {
          errorMessage = err.error.error;
        }
        this.toast.error({ detail: errorMessage, summary: "Error", duration: 5000 });
        console.log(err);
      }
    })
  }
  getAllhotel() {
    return this.addhotelser.ViewHotel().subscribe((respose) => {
      this.listofHotel = respose;
    })
  }
}
