import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminServiceService } from '../../../Services/admin-service.service';
import { AddHotelService } from '../../../Services/add-hotel.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-admin-home-page',
  templateUrl: './admin-home-page.component.html',
  styleUrl: './admin-home-page.component.css'
})
export class AdminHomePageComponent implements OnInit {
  Addhotel: FormGroup;
  listofHotel: any = [];
  id!: number;
  hotelCount!: number;
  branchCount!: number;
  PendingUserCount!: number;
  PendingVendorCount!: number;
  approvedUserCount!: number;
  approvedVendorCount!: number;
  NewArrivalsList: any = [];
  email!: string;
  constructor(private fb: FormBuilder, private adminser: AdminServiceService, private addhotelser: AddHotelService, private toast: NgToastService) {
    this.Addhotel = this.fb.group({
      HotelName: ['', Validators.required]
    })
  }
  ngOnInit() {
    this.initializeUserId();
    this.listHotel();
    this.TotalCountsHotel();
    this.TotalCountsBranch();
    this.PendingUsers();
    this.PendingVendors();
    this.ApprovedUsers();
    this.ApprovedVendors();
    this.NewArrivals();
  }

  get hotelname() {
    return this.Addhotel.get('HotelName');
  }

  formHotelsubmit() {
    console.log(this.Addhotel.value);
    let addhotl = { ...this.Addhotel.value, ApprovedUsersId: this.id }
    this.addhotelser.AddHotel(addhotl).subscribe({
      next: (response) => {
        this.Addhotel.reset();
        this.toast.success({ detail: "Hotel Added", summary: response.message, duration: 5000 });
        this.listHotel();
        this.TotalCountsHotel();
      },
      error: (err) => {
        this.toast.error({ detail: "ERROR", summary: "Something when wrong!", duration: 5000 });
        console.log(err);
      }
    })
  }

  private initializeUserId() {
    const idFromLocalStorage = this.adminser.getId();
    const emailfromstorage = this.adminser.getemail();
    this.id = idFromLocalStorage;
    this.email = emailfromstorage;
  }

  listHotel() {
    return this.addhotelser.ViewHotel().subscribe((respose) => {
      this.listofHotel = respose;
    })
  }

  TotalCountsHotel() {
    return this.adminser.gethotelCount().subscribe((respose) => {
      this.hotelCount = respose.totalHotel;
    })
  }
  TotalCountsBranch() {
    return this.adminser.getbranchCount().subscribe((respose) => {
      this.branchCount = respose.totalBranch;
    })
  }
  PendingUsers() {
    return this.adminser.getpendingUserCount().subscribe((respose) => {
      this.PendingUserCount = respose.pendingUSer;
    })
  }
  PendingVendors() {
    return this.adminser.getpendingVendorCount().subscribe((respose) => {
      this.PendingVendorCount = respose.pendingvendor;
    })
  }
  ApprovedUsers() {
    return this.adminser.getApprovedUserCount().subscribe((respose) => {
      this.approvedUserCount = respose.approvedUser;
    })
  }
  ApprovedVendors() {
    return this.adminser.getApprovedVendorCount().subscribe((respose) => {
      this.approvedVendorCount = respose.approvedVendor;
    })
  }
  NewArrivals() {
    return this.adminser.getAllNewArrivals().subscribe((respose) => {
      this.NewArrivalsList = respose;
    })
  }
}
