import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonEndpoint } from '../Constant/CommonEndpoint';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PendingUserServiceService {

  constructor(private http:HttpClient) { }

  // register user functionalities
  registerUser(userdetails:any){
    return this.http.post<any>(`${CommonEndpoint.pendingUserEndpoint}/PendingUser`,userdetails);
  }

  //get pending user
  public getpendingUser(): Observable<any> {
    return this.http.get(`${CommonEndpoint.pendingUserEndpoint}/DisplayPendingUser`);
  }

  // delete pending user
  public deletependingUser(id: number): Observable<any> {
    return this.http.delete(`${CommonEndpoint.pendingUserEndpoint}/DeletePendingUser?id=${id}`);
  }

}
