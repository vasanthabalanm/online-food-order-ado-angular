import { Component, OnInit } from '@angular/core';
import { AdminServiceService } from '../../../Services/admin-service.service';
import { AdminAlterServicesService } from '../../../Services/admin-alter-services.service';
import { NgToastService } from 'ng-angular-popup';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddHotelService } from '../../../Services/add-hotel.service';
import { AddMenuServiceService } from '../../../Services/add-menu-service.service';
import { response } from 'express';
import { Formvalidators } from '../../../Constant/Formvalidators';

@Component({
  selector: 'app-alter-hotel-branch',
  templateUrl: './alter-hotel-branch.component.html',
  styleUrl: './alter-hotel-branch.component.css'
})
export class AlterHotelBranchComponent implements OnInit {
  ApprovedUserData: any = [];
  approvedVendorData: any = [];
  Id!: number;
  Alterhotel: FormGroup;
  alterBranchForm: FormGroup;
  listofHotel: any = [];
  Role: any;
  hotelIds!: number;
  listbranches: any = [];
  alterbranchIds!: number;
  menuLists: any = [];
  menuId!: number;
  alterMenuForm: FormGroup;

  enteredValues: any = [];

  constructor(private fb: FormBuilder, private adminser: AdminServiceService, private alterSer: AdminAlterServicesService, private addhotelser: AddHotelService, private toast: NgToastService, private addMenuser: AddMenuServiceService) {
    this.Alterhotel = this.fb.group({
      HotelName: ['', Validators.required]
    })

    this.alterBranchForm = this.fb.group({
      HotelId: ['', Validators.required],
      BranchName: ['', Validators.required],
      BranchLocation: ['', Validators.required],
      BranchPhoneNumber: ['', [Validators.required, Validators.pattern(Formvalidators.phone)]]
    });

    this.alterMenuForm = this.fb.group({
      HotelBranchId: ['', Validators.required],
      MenuDetails: this.fb.array([])
    });
  }


  get hotelname() {
    return this.Alterhotel.get('HotelName');
  }
  get branchname() {
    return this.alterBranchForm.get('BranchName');
  }
  get branchlocation() {
    return this.alterBranchForm.get('BranchLocation');
  }
  get branchphonenumber() {
    return this.alterBranchForm.get('BranchPhoneNumber');
  }

  listHotel() {
    return this.addhotelser.ViewHotel().subscribe((respose) => {
      this.listofHotel = respose;
    })
  }

  ngOnInit() {
    this.initializeUserData();
    this.showApprovedUser();
    this.showApprovedVendor();
    this.listHotel();
    this.getAllbranch();
    this.getAllMenu();
  }

  //fetch the details from the localstorage
  private initializeUserData() {
    const IdFromLocalStorage = this.adminser.getId();
    const RoleFromLocal = this.adminser.getRole();
    this.Id = IdFromLocalStorage || 'null';
    this.Role = RoleFromLocal || 'null'
  }

  // display approved user
  public showApprovedUser() {
    this.alterSer.getApprovedUser().subscribe(
      result => {
        this.ApprovedUserData = result;
      },
      error => {
        console.error('Error fetching Approved user:', error);
      }
    );
  }
  //delete user
  deleteUser(id: number, email: any) {
    const isConfirmed = window.confirm("Are you sure you want to delete this user Permanantly?");
    if (isConfirmed) {
      this.alterSer.deleteApprovedUser(id, email).subscribe(response => {
        this.toast.success({ detail: "User Deleted Permanently", summary: response.message, duration: 3000 });
        this.showApprovedUser();
      });
    } else {
      this.toast.warning({ detail: "User Not deleted", summary: "canceled", duration: 3000 });
      console.log("User canceled deletion");
    }
  }

  // Vendor
  // display approved Vendor
  public showApprovedVendor() {
    this.alterSer.getApprovedVendor().subscribe(
      result => {
        this.approvedVendorData = result;
      },
      error => {
        console.error('Error fetching Approved Vendor:', error);
      }
    );
  }
  //delete vendor
  deleteVendor(id: number, email: any) {
    const isConfirmed = window.confirm("Are you sure you want to delete this user Permanantly?");
    if (isConfirmed) {
      this.alterSer.deleteApprovedVendor(id, email).subscribe(response => {
        this.toast.success({ detail: "Vendor Deleted Permanently", summary: response.message, duration: 3000 });
        this.showApprovedVendor();
      });
    } else {
      this.toast.warning({ detail: "Vendor Not deleted", summary: "canceled", duration: 3000 });
      console.log("User canceled deletion");
    }
  }

  //hotels

  setFormValues(hotel: any) {
    this.hotelIds = hotel.id;
    this.Alterhotel.patchValue({
      HotelName: hotel.hotelName,
    });
  }

  formHotelsubmit() {
    let addhotl = { id: this.hotelIds, ...this.Alterhotel.value, ApprovedUsersId: this.Id }
    this.alterSer.UpdateHotel(addhotl).subscribe({
      next: (response) => {
        console.log(response);
        this.toast.success({ detail: "HotelUpdated", summary: "Updated", duration: 3000 });
        this.Alterhotel.reset();
      },
      error: (err) => {
        this.toast.error({ detail: "ERROR", summary: "Something when wrong!", duration: 5000 });
        console.log(err);
      }
    })
  }
  updateHotels() {
    this.listHotel();
  }

  //delete hotel
  deleteHotel(hotelvalue: any) {
    console.log(hotelvalue)
    const isConfirmed = window.confirm("Are you sure you want to delete this Hotel Permanantly?");
    if (isConfirmed) {
      this.alterSer.deletehotel(hotelvalue.id, this.Role).subscribe(response => {
        this.toast.success({ detail: "Hotel Deleted Permanently", summary: response.message, duration: 3000 });
        this.listHotel();
      });
    } else {
      this.toast.warning({ detail: "Hotel Not deleted", summary: "canceled", duration: 3000 });
      console.log("Hotel cancelled for Delete");
    }
  }

  //branch
  getAllbranch() {
    this.addMenuser.getBranch(this.Id, this.Role).subscribe((respose) => {
      this.listbranches = respose;
    });
  }

  // setalterBranchValues
  setalterBranchValues(branch: any) {
    console.log(branch);
    this.alterbranchIds = branch.id;
    this.alterBranchForm.patchValue({
      HotelId: branch.hotelId,
      BranchName: branch.branchName,
      BranchLocation: branch.branchLocation,
      BranchPhoneNumber: branch.branchPhoneNumber
    });
  }

  // alter brach details
  alterbranchDetails() {
    let alterbrnch = { id: this.alterbranchIds, ...this.alterBranchForm.value }
    console.log(alterbrnch)
    this.alterSer.UpdatehotelBranch(alterbrnch).subscribe({
      next: (response) => {
        console.log(response);
        this.toast.success({ detail: "Branch Updated", summary: "Updated", duration: 3000 });

      },
      error: (err) => {
        this.toast.error({ detail: "ERROR", summary: "Something when wrong!", duration: 5000 });
        console.log(err);
      }
    })
  }

  //delete branch
  deletebranch(branchdetails: any) {
    console.log(branchdetails)
    const isConfirmed = window.confirm("Are you sure you want to delete this Branch Permanantly?");
    if (isConfirmed) {
      this.alterSer.deletehotelBranch(branchdetails.id, branchdetails.hotelId).subscribe(response => {
        this.toast.success({ detail: "Hotel Deleted Permanently", summary: response.message, duration: 3000 });
        this.getAllbranch();
      });
    } else {
      this.toast.warning({ detail: "Branch Not deleted", summary: "canceled", duration: 3000 });
      console.log("Hotelbrach cancelled for Delete");
    }
  }

  updateHotelbranch() {
    this.getAllbranch();
  }

  //menu details
  getAllMenu() {
    return this.alterSer.getMenuDetails().subscribe((response) => {
      this.menuLists = response;
    })
  }

  // fetch to pass the menu id to modal
  setalterMenuValues(menu: any) {
    // Clear existing form array data
    this.menuId = menu.id
    const menuDetailsArray = this.alterMenuForm.get('MenuDetails') as FormArray;
    menuDetailsArray.clear();

    // Set values for the existing form group
    menuDetailsArray.push(
      this.fb.group({
        Id: this.menuId,
        MenuItems: [menu.menuItems, Validators.required],
        MenuQuantity: [menu.menuQuantity, Validators.required],
        Price: [menu.price, Validators.required],
        HotelBranchId: [menu.hotelBranchId, Validators.required]
      })
    );

    // Set the value for the select dropdown
    this.alterMenuForm.patchValue({
      HotelBranchId: menu.hotelBranchId
    });
  }

  // delete menu
  deleteMenu(entireMenu: any) {
    const isConfirmed = window.confirm("Are you sure you want to delete this Branch Permanantly?");
    if (isConfirmed) {
      this.alterSer.deleteMenu(entireMenu.id, entireMenu.hotelBranchId).subscribe(() => {
        this.toast.success({ detail: "Menu Deleted Permanently", summary: "Done", duration: 3000 });
        this.getAllMenu();
      });
    } else {
      this.toast.warning({ detail: "MenuItem Not deleted", summary: "canceled", duration: 3000 });
      console.log("MenuItem cancelled for Delete");
    }
  }

  // form array controls
  getMenuDetailsControls() {
    return (this.alterMenuForm.get('MenuDetails') as FormArray).controls;
  }

  finderrorinFormarray(index: number) {
    this.enteredValues = this.alterMenuForm.get('MenuDetails') as FormArray;
    const formgrop = this.enteredValues.controls[index] as FormGroup;
    return formgrop;
  }

  // submit update menu details
  alterMenuDetails() {
    const menuDetailsArray = this.alterMenuForm.get('MenuDetails') as FormArray;
    const MenuupdatedValue = menuDetailsArray.value;
    for (let i = 0; i < MenuupdatedValue.length; i++) {
      const menuItem = MenuupdatedValue[i];
      this.alterSer.UpdatemenuDetails(menuItem).subscribe(
        (response) => {
          console.log(response);
          if (i === MenuupdatedValue.length - 1) {
            this.toast.success({ detail: "MenuUpdated", summary: "Updated", duration: 3000 });
            this.getAllMenu();
          }
        }, (error) => {
          this.toast.error({ detail: "ERROR", summary: "Something when wrong!", duration: 5000 });
          console.log(error);
        }
      )
    }

  }

}
