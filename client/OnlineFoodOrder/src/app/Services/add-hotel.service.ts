import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonEndpoint } from '../Constant/CommonEndpoint';

@Injectable({
  providedIn: 'root'
})
export class AddHotelService {

  constructor(private http:HttpClient) { }

  //add hotel
  AddHotel(hotelname:any){
    return this.http.post<any>(`${CommonEndpoint.addHotel}/AddHotel`,hotelname);
  }

  //view hotel
  ViewHotel(){
    return this.http.get<any>(`${CommonEndpoint.addHotel}/ViewHotel`);
  }
}
