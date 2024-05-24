import { Component } from '@angular/core';
import { AdminServiceService } from '../../../Services/admin-service.service';

@Component({
  selector: 'app-user-side-bar',
  templateUrl: './user-side-bar.component.html',
  styleUrl: './user-side-bar.component.css'
})
export class UserSideBarComponent {
  role!:string
  constructor(private adminser:AdminServiceService){}
  ngOnInit(){
    this.initializeUserData();
  }

  //fetch the details from the localstorage
  private initializeUserData() {
    const roleFromLocalStorage = this.adminser.getRole();
    console.log(roleFromLocalStorage);
    this.role = roleFromLocalStorage || 'null';
  }
  logout(){
    this.adminser.logout();
  }
}
