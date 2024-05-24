import { Component, OnInit } from '@angular/core';
import { AdminServiceService } from '../../../Services/admin-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent implements OnInit {
  role!: string
  constructor(private adminser: AdminServiceService, private router: Router) { }
  ngOnInit() {
    this.initializeUserData();

    if (this.role === 'Admin') {
      this.router.navigate(['/admin-dashboard/admin-home']);
    }
    else if (this.role === 'User') {
      this.router.navigate(['/admin-dashboard/user-home']);
    }
    else if (this.role === 'Vendor') {
      this.router.navigate(['/admin-dashboard/vendor-home']);
    }
  }

  //fetch the details from the localstorage
  private initializeUserData() {
    const roleFromLocalStorage = this.adminser.getRole();
    console.log(roleFromLocalStorage);
    this.role = roleFromLocalStorage || 'null';
  }
}
