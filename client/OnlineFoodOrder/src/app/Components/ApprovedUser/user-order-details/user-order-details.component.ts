import { Component, OnInit } from '@angular/core';
import { UserViewMenuListServiceService } from '../../../Services/user-view-menu-list-service.service';
import { AdminServiceService } from '../../../Services/admin-service.service';
import { response } from 'express';

@Component({
  selector: 'app-user-order-details',
  templateUrl: './user-order-details.component.html',
  styleUrl: './user-order-details.component.css'
})
export class UserOrderDetailsComponent implements OnInit {

  pendingOrderList: any = [];
  approvedOrderList: any = [];
  Id!: number;
  constructor(private menuorder: UserViewMenuListServiceService, private adminser: AdminServiceService) { }
  ngOnInit() {
    this.Id = this.adminser.getId();
    console.log(this.Id);
    this.getPendingorder();
    this.getApprovedorder();
  }

  getPendingorder() {
    this.menuorder.getPendingOrder(this.Id).subscribe((response) => {
      console.log(response);
      this.pendingOrderList = response;
      console.log(this.pendingOrderList);
    })
  }

  getApprovedorder() {
    this.menuorder.getApprovedOrder(this.Id).subscribe((response) => {
      console.log(response);
      this.approvedOrderList = response;
      console.log(this.approvedOrderList);
    })
  }
}
