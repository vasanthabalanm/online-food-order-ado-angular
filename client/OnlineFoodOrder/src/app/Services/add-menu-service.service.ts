import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonEndpoint } from '../Constant/CommonEndpoint';

@Injectable({
  providedIn: 'root'
})
export class AddMenuServiceService {
  constructor(private http:HttpClient) { }

  // add menu
  AddMenu(menudetails:any){
    console.log(menudetails);
    return this.http.post(`${CommonEndpoint.addmenu}/Addmenu`,menudetails);
  }

  //get branch
  getBranch(branchid:number,role:string){
    return this.http.get(`${CommonEndpoint.addBranch}/DisplayBranch?id=${branchid}&role=${role}`);
  }
}
