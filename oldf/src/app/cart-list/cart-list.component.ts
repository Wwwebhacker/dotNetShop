import { Component } from '@angular/core';
import { CartService } from '../services/cart.service';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-cart-list',
  templateUrl: './cart-list.component.html',
  styleUrls: ['./cart-list.component.css']
})
export class CartListComponent {
  products: Product[] = [];
  constructor(private cartService: CartService){
    cartService.subscribeToProducts((products: Product[]) => {
      this.products = products
    })
    
  }

  removeFromCart(product: Product){
    this.cartService.removeFrom(product);
  }
  buy(){
    this.cartService.buy().subscribe(() => {
      
    });
  }
}
