import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonEndpoint } from '../Constant/CommonEndpoint';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminAlterServicesService {

  constructor(private http: HttpClient) { }

  //get approved user
  public getApprovedUser(): Observable<any> {
    return this.http.get(`${CommonEndpoint.adminEndpoints}/OnlyApprovedUsers`);
  }

  // delete approved user
  public deleteApprovedUser(id: number, email: any): Observable<any> {
    return this.http.delete(`${CommonEndpoint.adminEndpoints}/DeleteUserDetails?id=${id}&email=${email}`);
  }

  //get approved vendor
  public getApprovedVendor(): Observable<any> {
    return this.http.get(`${CommonEndpoint.adminEndpoints}/OnlyApprovedVendors`);
  }

  // delete approved vendor
  public deleteApprovedVendor(id: number, email: any): Observable<any> {
    return this.http.delete(`${CommonEndpoint.adminEndpoints}/DeleteUserDetails?id=${id}&email=${email}`);
  }

  //update hotel
  public UpdateHotel(hotelValues: any) {
    return this.http.put(`${CommonEndpoint.addHotel}/HotelUpdate`, hotelValues);
  }
  // delete hotel
  public deletehotel(id: number, role: any): Observable<any> {
    return this.http.delete(`${CommonEndpoint.addHotel}/DeleteHotelDetails?id=${id}&role=${role}`);
  }

  //update hotel
  public UpdatehotelBranch(hotelValues: any) {
    return this.http.put(`${CommonEndpoint.addBranch}/UpdateBranch`, hotelValues);
  }

  // delete hotelbranch
  public deletehotelBranch(id: number, hotelid: number): Observable<any> {
    return this.http.delete(`${CommonEndpoint.addBranch}/DeleteBranch?id=${id}&hotelid=${hotelid}`);
  }

  //get menu details
  public getMenuDetails(): Observable<any> {
    return this.http.get(`${CommonEndpoint.addmenu}/ViewMenu`);
  }

  // update menu details
  public UpdatemenuDetails(menuValues:any):Observable<any>{
    return this.http.put(`${CommonEndpoint.addmenu}/updateMenu`,menuValues)
  }

  // delete menuitems
  public deleteMenu(menuid:number,hotelbranchId:number){
    return this.http.delete(`${CommonEndpoint.addmenu}/DeleteMenu?id=${menuid}&hotelbranchid=${hotelbranchId}`)
  }

}
