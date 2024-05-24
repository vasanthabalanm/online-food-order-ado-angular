import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonEndpoint } from '../Constant/CommonEndpoint';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PendingVendorServiceService {

  constructor(private http:HttpClient) { }

  registerVendor(vendordetails:any){
    return this.http.post<any>(`${CommonEndpoint.pendingUserEndpoint}/PendingVendor`,vendordetails);
  }

  //get pending vendor
  public getpendingVendor(): Observable<any> {
    return this.http.get(`${CommonEndpoint.pendingUserEndpoint}/DisplayPendingVendor`);
  }

  // delete pending vendor
  public deletependingVendor(id: number): Observable<any> {
    return this.http.delete(`${CommonEndpoint.pendingUserEndpoint}/DeletePendingVendor?id=${id}`);
  }
}
