import { Component } from '@angular/core';
import { CartService } from '../services/cart.service';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  products: Product[] = []

  constructor(private service: CartService){
    service.subscribeToProducts((products: Product[]) => {
      this.products = products
    })
  }
}
