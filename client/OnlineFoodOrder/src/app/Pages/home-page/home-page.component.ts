import { Component, OnInit } from '@angular/core';
import { UserViewMenuListServiceService } from '../../Services/user-view-menu-list-service.service';
import { response } from 'express';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {

  Menulist: any = [];
  constructor(private menuSer: UserViewMenuListServiceService) { }
  ngOnInit() {
    this.viewMenu();
  }

  public viewMenu() {
    this.menuSer.getUserMenu().subscribe((response) => {
      this.Menulist = response;
    })
  }

}
