import { Component, OnInit } from '@angular/core';
import { UserViewMenuListServiceService } from '../../../Services/user-view-menu-list-service.service';
import { AdminServiceService } from '../../../Services/admin-service.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-show-pending-order',
  templateUrl: './show-pending-order.component.html',
  styleUrl: './show-pending-order.component.css'
})
export class ShowPendingOrderComponent implements OnInit {
  OrderList:any=[];
  Role!:string;
  constructor(private viewOrder:UserViewMenuListServiceService, private adminser:AdminServiceService,private toast:NgToastService)
  {}
  ngOnInit() {
    this.fetchOrder();
    this.Role = this.adminser.getRole();
  }

  fetchOrder(){
    this.viewOrder.getOrderMenu().subscribe((response)=>{
      this.OrderList = response;
      console.log(response);
    })
  }

  approveOrder(values: any): void {
    const id: number = values; 
    const role: string = this.Role; 

    console.log(`Approving order with ID ${id} for role ${role}`);

    this.viewOrder.sendEmail(id, role).subscribe(
        (response) => {
            console.log('Response:', response);
            this.toast.success({ detail: 'Approved', summary: 'Order Approved', duration: 3000 });    
            this.fetchOrder();
        },
        (error) => {
            console.error('Error while approving order:', error);
        }
    );
}



}
