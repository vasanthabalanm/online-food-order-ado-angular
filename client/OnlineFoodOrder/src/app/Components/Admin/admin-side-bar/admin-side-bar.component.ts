import { Component, OnInit } from '@angular/core';
import { AdminServiceService } from '../../../Services/admin-service.service';

@Component({
  selector: 'app-admin-side-bar',
  templateUrl: './admin-side-bar.component.html',
  styleUrl: './admin-side-bar.component.css'
})
export class AdminSideBarComponent implements OnInit {
  role!: string
  constructor(private adminser: AdminServiceService) { }
  ngOnInit() {
    this.initializeUserData();
  }

  //fetch the details from the localstorage
  private initializeUserData() {
    const roleFromLocalStorage = this.adminser.getRole();
    console.log(roleFromLocalStorage);
    this.role = roleFromLocalStorage || 'null';
  }
  logout() {
    this.adminser.logout();
  }
}