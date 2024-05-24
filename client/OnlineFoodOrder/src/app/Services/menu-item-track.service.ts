import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MenuItemTrackService {
  private cartItems: any[] = [];

  getCartItems(): any[] {
    return this.cartItems;
  }

  addToCart(item: any): void {
    this.cartItems.push(item);
  }

  removeFromCart(index: number): void {
    this.cartItems.splice(index, 1);
  }

  clearCart(): void {
    this.cartItems = [];
  }
}
