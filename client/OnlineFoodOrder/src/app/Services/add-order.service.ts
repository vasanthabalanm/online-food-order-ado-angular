import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonEndpoint } from '../Constant/CommonEndpoint';

@Injectable({
  providedIn: 'root'
})
export class AddOrderService {

  constructor(private http:HttpClient) { }

  PostOrder(values:any){
    return this.http.post<any>(`${CommonEndpoint.pendingOrder}/AddOrder`,values);
  }
}
