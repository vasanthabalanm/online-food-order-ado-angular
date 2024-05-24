import { Component, OnInit } from '@angular/core';
import { AddBranchServiceService } from '../../../Services/add-branch-service.service';
import { NgToastService } from 'ng-angular-popup';
import { AddHotelService } from '../../../Services/add-hotel.service';
import { Formvalidators } from '../../../Constant/Formvalidators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-branch',
  templateUrl: './add-branch.component.html',
  styleUrl: './add-branch.component.css'
})
export class AddBranchComponent implements OnInit {
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
  ngOnInit() {
    this.getAllhotel();
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
