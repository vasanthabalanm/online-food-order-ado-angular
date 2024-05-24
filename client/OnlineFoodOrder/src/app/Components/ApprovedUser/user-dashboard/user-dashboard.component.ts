import { Component } from '@angular/core';
import { UserViewMenuListServiceService } from '../../../Services/user-view-menu-list-service.service';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MenuItemTrackService } from '../../../Services/menu-item-track.service';
import { NgToastService } from 'ng-angular-popup';
import { AdminServiceService } from '../../../Services/admin-service.service';
import { AddOrderService } from '../../../Services/add-order.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css'
})
export class UserDashboardComponent {
  Menulist: any = [];
  orderCount: number = 1;
  AddOrder: FormGroup;
  pendingMenu: any = [];
  approvedUsersId!: number;

  filterItems: FormGroup;
  filteredList: any = []

  email!: string;

  constructor(private menuSer: UserViewMenuListServiceService, private fb: FormBuilder, private trackMenu: MenuItemTrackService, private toast: NgToastService, private adminser: AdminServiceService, private pendingOrder: AddOrderService, private router: Router) {
    this.AddOrder = this.fb.group({
      menuItems: [],
      hotelBranchId: [],
      price: [''],
      branchLocation: [],
      menuDetailsId: [],
      orderStatus: [''],
      quantityOfOrder: new FormControl(this.orderCount, Validators.required)
    });

    this.filterItems = this.fb.group({
      menuItems: [],
      branchLocation: []
    })
  }
  ngOnInit() {
    this.viewMenu();
    this.getUserId();
  }

  get counts() {
    return this.AddOrder.get('quantityOfOrder');
  }
  public viewMenu() {
    this.menuSer.getUserMenu().subscribe((response) => {
      this.Menulist = response;
    })
  }

  Orders(selectedValues: any) {
    console.log(selectedValues)
    this.AddOrder.patchValue({
      menuItems: selectedValues.menuItems,
      hotelBranchId: selectedValues.hotelBranchId,
      price: selectedValues.price,
      branchLocation: selectedValues.branchLocation,
      menuDetailsId: selectedValues.menuItemId,
      orderStatus: "Pending",
    });
  }

  incrementOrderCount() {
    this.orderCount++;
    this.AddOrder.get('quantityOfOrder')?.setValue(this.orderCount);
  }

  decrementOrderCount() {
    if (this.orderCount > 1) {
      this.orderCount--;
      this.AddOrder.get('quantityOfOrder')?.setValue(this.orderCount);
    }
  }

  // form submit counts
  formOrdersubmit() {
    if (this.AddOrder.valid) {
      let vlaue = this.AddOrder.value
      console.log(vlaue);
      let defaultprice = vlaue.price
      let orderCount = vlaue.quantityOfOrder
      let totalPrice = orderCount * defaultprice;
      let usrid = this.approvedUsersId
      let menuid = vlaue.menuDetailsId
      let status = vlaue.orderStatus
      console.log(menuid)
      let CompleteValue = {
        approvedUsersId: usrid,
        menuItems: vlaue.menuItems,
        price: totalPrice,
        hotelBranchId: vlaue.hotelBranchId,
        quantityOfOrder: vlaue.quantityOfOrder,
        branchLocation: vlaue.branchLocation,
        menuDetailsId: menuid,
        orderStatus: status
      }

      this.addToCart(CompleteValue);
    }
    else {
      this.toast.error({ detail: "ERROR", summary: "please Mention the count!", duration: 5000 });
    }

  }

  // close model
  closeModel() {
    this.AddOrder.get('quantityOfOrder')?.setValue(this.orderCount = 1);
  }

  //track menu service
  addToCart(item: any): void {
    this.trackMenu.addToCart(item);
  }

  removeFromCart(index: number): void {
    this.trackMenu.removeFromCart(index);
  }

  getCartItems(): any[] {
    this.pendingMenu = this.trackMenu.getCartItems()
    return this.trackMenu.getCartItems();
  }

  // place order
  PlaceOrder() {
    const cartItems = this.getCartItems();
    console.log(cartItems);

    for (const item of cartItems) {
      const { approvedUsersId, hotelBranchId, menuDetailsId, quantityOfOrder, price, orderStatus } = item;

      const orderToAdd = {
        approvedUsersId,
        hotelBranchId,
        menuDetailsId,
        quantityOfOrder,
        price,
        orderStatus
      };
      console.log(orderToAdd)
      this.pendingOrder.PostOrder(orderToAdd).subscribe(
        (response) => {
          console.log(response);
          // this.toast.success({ detail: "Order Added", summary: "Wait for approval", duration: 3000 });
          this.router.navigate(['/admin-dashboard/payment']);
        },
        (error) => {
          this.toast.error({ detail: "ERROR", summary: "Something went wrong!", duration: 5000 });
          console.log(error);
        }
      );
    }
  }


  // get userid
  getUserId() {
    this.approvedUsersId = this.adminser.getId();
    this.email = this.adminser.getemail();
    console.log(this.approvedUsersId);
  }


  //fetched result
  ItemValues() {
    let menuname = this.filterItems.get('menuItems')?.value;
    let branchlocate = this.filterItems.get('branchLocation')?.value;
    this.menuSer.filterMenuItems(menuname, branchlocate).subscribe((respose) => {
      this.filteredList = respose;
      console.log(this.filteredList)
    });
  }
}

