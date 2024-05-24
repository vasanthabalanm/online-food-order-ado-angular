import { Component, OnInit } from '@angular/core';
import { PendingUserServiceService } from '../../../Services/pending-user-service.service';
import { NgToastService } from 'ng-angular-popup';
import { PendingVendorServiceService } from '../../../Services/pending-vendor-service.service';
import { AdminServiceService } from '../../../Services/admin-service.service';

@Component({
  selector: 'app-list-pending-user',
  templateUrl: './list-pending-user.component.html',
  styleUrl: './list-pending-user.component.css'
})
export class ListPendingUserComponent implements OnInit {
  pendingUserData: any = [];
  pendingVendorData: any = [];

  constructor(private pendingser: PendingUserServiceService, private pendingVendor: PendingVendorServiceService, private adminser: AdminServiceService, private toast: NgToastService, private pendeingVendorser: PendingVendorServiceService) {

  }
  ngOnInit() {
    this.showpendingUser();
    this.showpendingVendor();
  }

  // display pending user
  public showpendingUser() {
    this.pendingser.getpendingUser().subscribe(
      result => {
        this.pendingUserData = result;
      },
      error => {
        console.error('Error fetching pending user:', error);
      }
    );
  }

  // approve user action
  approveUser(getvalues: any) {
    this.adminser.addPendingUser(getvalues).subscribe({
      next: (response) => {
        console.log(response);
        this.toast.success({ detail: "User Added", summary: response.message, duration: 3000 });
      },
      error: (err) => {
        this.toast.error({ detail: "ERROR", summary: "Something when wrong!", duration: 3000 });
        console.log(err);
      }
    }),
      this.pendingser.deletependingUser(getvalues.id).subscribe(response => {
        this.showpendingUser();
      })
  }

  // delete user action
  deleteUser(id: number) {
    console.log(id);
    this.pendingser.deletependingUser(id).subscribe(response => {
      this.toast.warning({ detail: "User Declined", summary: response.message, duration: 3000 });
      this.showpendingUser();
    })
  }

  // for vendors
  // display pending user
  public showpendingVendor() {
    this.pendeingVendorser.getpendingVendor().subscribe(
      result => {
        this.pendingVendorData = result;
        console.log(this.pendingVendorData)
      },
      error => {
        console.error('Error fetching pending user:', error);
      }
    );
  }

  // approve user action
  approveVendor(getvalues: any) {
    this.adminser.addPendingUser(getvalues).subscribe({
      next: (response) => {
        console.log(response);
        this.toast.success({ detail: "Vendor Added", summary: response.message, duration: 3000 });
      },
      error: (err) => {
        this.toast.error({ detail: "ERROR", summary: "Something when wrong!", duration: 3000 });
        console.log(err);
      }
    }),
      this.pendeingVendorser.deletependingVendor(getvalues.id).subscribe(() => {
        this.showpendingVendor();
      })
  }

  // delete user action
  deleteVendor(id: number) {
    console.log(id);
    this.pendeingVendorser.deletependingVendor(id).subscribe(response => {
      this.toast.warning({ detail: "Vendor Declined", summary: response.message, duration: 3000 });
      this.showpendingVendor();
    })
  }
}
