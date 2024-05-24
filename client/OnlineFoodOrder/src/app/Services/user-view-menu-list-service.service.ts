import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonEndpoint } from '../Constant/CommonEndpoint';

@Injectable({
  providedIn: 'root'
})
export class UserViewMenuListServiceService {

  constructor(private http:HttpClient) { }

  public getUserMenu():Observable<any>{
    return this.http.get(`${CommonEndpoint.userMenu}/UserViewMenu`);
  }

 
  public filterMenuItems(menuName:string,branchLocation:string):Observable<any>{
    return this.http.get(`${CommonEndpoint.userMenu}/FilterbyLocation?menuName=${menuName}&branchLocation=${branchLocation}`)
  }

  public getPendingOrder(Id:number):Observable<any>{
    return this.http.get(`${CommonEndpoint.userMenu}/UserViewPendingOrder?id=${Id}`)
  }

  //get approved
  public getApprovedOrder(Id:number):Observable<any>{
    return this.http.get(`${CommonEndpoint.userMenu}/UserViewApprovedOrder?id=${Id}`)
  }

  //get Pendind order
  public getOrderMenu():Observable<any>{
    return this.http.get(`${CommonEndpoint.adminEndpoints}/ViewOrders`);
  }

  //send email
  public sendEmail(id: number, role: string): Observable<any> {
    const url = `${CommonEndpoint.adminEndpoints}/OrderStatusChange?orderid=${id}&role=${role}`;
    return this.http.put<any>(url, {});
  }
  
  
}
