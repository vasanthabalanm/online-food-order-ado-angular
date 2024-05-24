import { Component, OnInit } from '@angular/core';
import { AdminServiceService } from '../../../Services/admin-service.service';

@Component({
  selector: 'app-vendor-sidebar',
  templateUrl: './vendor-sidebar.component.html',
  styleUrl: './vendor-sidebar.component.css'
})
export class VendorSidebarComponent implements OnInit {
  role!: string
  constructor(private adminser: AdminServiceService) { }
  ngOnInit() {
    this.initializeUserData();
  }

  //fetch the details from the localstorage
  private initializeUserData() {
    const roleFromLocalStorage = this.adminser.getRole();
    this.role = roleFromLocalStorage || 'null';
  }
  logout() {
    this.adminser.logout();
  }
}