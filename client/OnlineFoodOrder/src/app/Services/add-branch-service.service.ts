import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonEndpoint } from '../Constant/CommonEndpoint';

@Injectable({
  providedIn: 'root'
})
export class AddBranchServiceService {

  constructor(private http:HttpClient) { }

  // add menu
  AddBrach(branchdetails:any){
    return this.http.post<any>(`${CommonEndpoint.addBranch}/AddBranch`,branchdetails);
  }
}
