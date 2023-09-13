import { Component, EventEmitter, Output } from '@angular/core';
import { ProductsService } from '../services/products.service';
import { Product } from '../models/product.model';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {

  products: Product[] = [];
  intervalId = -1;

  constructor(private service: ProductsService, public cartService: CartService) { }
  
  ngOnInit(){
    this.load();
    // this.intervalId = window.setInterval(() => this.load(), 5000);
  }

  ngOnDestroy(){
    clearInterval(this.intervalId);
  }

  load() {
    this.service.getProducts().subscribe((products) => {
      this.products = products;
    })
  }

  addToCart(product: Product){
    this.cartService.addToCart(product);
  }
}
