import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddMenuServiceService } from '../../../Services/add-menu-service.service';
import { AdminServiceService } from '../../../Services/admin-service.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-add-menu-item',
  templateUrl: './add-menu-item.component.html',
  styleUrl: './add-menu-item.component.css'
})
export class AddMenuItemComponent implements OnInit {

  addMenuForm: FormGroup
  listbranches: any = [];
  getId!: number;
  getRole!: string;
  enteredValues: any = [];


  constructor(private fb: FormBuilder, private addMenuser: AddMenuServiceService, private adminser: AdminServiceService, private toast: NgToastService) {
    this.addMenuForm = this.fb.group({
      HotelBranchId: ['', Validators.required],
      MenuDetails: this.fb.array([])
    });
  }

  get hotelbranchid() {
    return this.addMenuForm.get('HotelBranchId');
  }

  ngOnInit() {
    this.IntializeId();
    this.IntializeRole();
    this.getAllbranch();
  }

  // fect by ngoninit branch name
  getAllbranch() {
    this.addMenuser.getBranch(this.getId, this.getRole).subscribe((respose) => {
      this.listbranches = respose;
    })
    const menuData = this.addMenuForm.get('MenuDetails') as FormArray
    menuData.clear();
  }

  // add menu data in form array
  addMenuData() {
    const branchid = this.addMenuForm.get('HotelBranchId');
    const menuData = this.addMenuForm.get('MenuDetails') as FormArray
    menuData.push(
      this.fb.group({
        MenuItems: ['', [Validators.required, this.nonamespaces]],
        MenuQuantity: ['', Validators.required],
        Price: ['', Validators.required],
        HotelBranchId: [branchid?.value ? +branchid.value : null, [Validators.required, Validators.pattern(/^[1-9][0-9]*$/)]], // Positive integers
      })
    );
  }

  // remove whenever it is not needed
  removeMenuData(indexValue: number) {
    const selectMenuId = this.addMenuForm.get('MenuDetails') as FormArray
    selectMenuId.removeAt(indexValue);
  }

  // check the error control by using this meathod
  getMenuDetailsControls() {
    return (this.addMenuForm.get('MenuDetails') as FormArray).controls;
  }

  finderrorinFormarray(index: number) {
    this.enteredValues = this.addMenuForm.get('MenuDetails') as FormArray;
    const formgrop = this.enteredValues.controls[index] as FormGroup;
    return formgrop;
  }

  nonamespaces(empname: AbstractControl) {
    const employename = empname.value;
    if (employename && ((employename.startsWith(' ') || (employename.endsWith(' '))))) {
      return { nonamespaces: true }
    }
    return null;
  }

  // addd menu
  addMenuDetails() {
    const menuDetailsArray = this.addMenuForm.get('MenuDetails') as FormArray;

    if (menuDetailsArray && menuDetailsArray.length === 0) {
      alert('Please enter the menu details before you submit!');
      console.log('Please enter value');
    } else {
      const menuItems = menuDetailsArray.value;

      // Iterate through each menu item and send to the backend
      for (let i = 0; i < menuItems.length; i++) {
        const menuItem = menuItems[i];
        console.log(menuItem)

        this.addMenuser.AddMenu(menuItem).subscribe(
          (response) => {
            console.log(`Response for item at index ${i}:`, response);

            // If this is the last item, reset the form and show success message
            if (i === menuItems.length - 1) {
              this.toast.success({ detail: "Menu(s) Added", summary: "Menu(s) Added Successfully", duration: 5000 });
              const menuData = this.addMenuForm.get('MenuDetails') as FormArray
              menuData.clear();
            }
          }, (error) => {
            console.error(`Error for item at index ${i}:`, error);

            let errorMessage = "Something went wrong!";
            if (error && error.error && error.error.error) {
              errorMessage = error.error.error;
            }

            this.toast.error({ detail: errorMessage, summary: "Error", duration: 5000 });
          }
        );
      }
    }
  }

  IntializeId() {
    const adminid = this.adminser.getId();
    this.getId = adminid
  }
  IntializeRole() {
    const adminRole = this.adminser.getRole();
    this.getRole = adminRole;
  }
}
