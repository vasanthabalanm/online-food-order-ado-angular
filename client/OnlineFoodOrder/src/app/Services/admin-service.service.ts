import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonEndpoint } from '../Constant/CommonEndpoint';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminServiceService {

  constructor(private http: HttpClient, private router: Router) { }

  login(logindetails: any) {
    console.log(logindetails)
    return this.http.post<any>(`${CommonEndpoint.adminEndpoints}/login`, logindetails);
  }
  //<
  //   setStorageValues(setId:string,setemail:string,setRole:string){
  //     localStorage.setItem('Id',setId)
  //     localStorage.setItem('email',setemail)
  //     localStorage.setItem('role',setRole)    
  //   }

  //   getRole(){
  //     return localStorage.getItem('role');
  //   }

  //   isLogin():boolean{
  //     return !!localStorage.getItem('role');
  //   } 

  //   getemail(){
  //     return localStorage.getItem('email');
  //   }

  //   logout(){
  //     localStorage.clear();
  //     this.router.navigate(['login'])
  //   }
  //>
  setStorageValues(setId: string, setemail: string, setRole: string,setOldpass:string) {
    const encodedData = btoa(JSON.stringify({ id: setId, email: setemail, role: setRole,oldPassword:setOldpass }));
    localStorage.setItem('userData', encodedData);
  }

  getRole() {
    const encodedData = localStorage.getItem('userData');
    if (encodedData) {
      const decodedData = JSON.parse(atob(encodedData));
      return decodedData.role;
    }
    return null;
  }

  getOldPass() {
    const encodedData = localStorage.getItem('userData');
    if (encodedData) {
      const decodedData = JSON.parse(atob(encodedData));
      return decodedData.oldPassword;
    }
    return null;
  }

  getId() {
    const encodedData = localStorage.getItem('userData');
    if (encodedData) {
      const decodedData = JSON.parse(atob(encodedData));
      return decodedData.id;
    }
    return null;
  }

  isLogin(): boolean {
    return !!localStorage.getItem('userData');
  }

  getemail() {
    const encodedData = localStorage.getItem('userData');
    if (encodedData) {
      const decodedData = JSON.parse(atob(encodedData));
      return decodedData.email;
    }
    return null;
  }

  logout() {
    localStorage.removeItem('userData');
    this.router.navigate(['login']);
  }

  addPendingUser(userdetails: any) {
    return this.http.post<any>(`${CommonEndpoint.adminEndpoints}/RegisterUser`, userdetails);
  }

  gethotelCount(){
    return this.http.get<any>(`${CommonEndpoint.adminEndpoints}/TotalHotelCounts`);
  }
  getbranchCount(){
    return this.http.get<any>(`${CommonEndpoint.adminEndpoints}/TotalBranchCounts`);
  }
  getpendingUserCount(){
    return this.http.get<any>(`${CommonEndpoint.adminEndpoints}/PendingUserCounts`);
  }
  getpendingVendorCount(){
    return this.http.get<any>(`${CommonEndpoint.adminEndpoints}/VendorPendingCounts`);
  }
  getApprovedUserCount(){
    return this.http.get<any>(`${CommonEndpoint.adminEndpoints}/ApprovedUserCounts`);
  }
  getApprovedVendorCount(){
    return this.http.get<any>(`${CommonEndpoint.adminEndpoints}/approvedVendorCounts`);
  }
  getAllNewArrivals(){
    return this.http.get<any>(`${CommonEndpoint.adminEndpoints}/NewArrivals`);
  }

  //update password
  public UpdatePassword(PasswordValues:any):Observable<any>{
    console.log(PasswordValues);
    return this.http.put(`${CommonEndpoint.adminEndpoints}/UpdatePassword`,PasswordValues)
  }
}
