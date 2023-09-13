import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-p-card',
  templateUrl: './p-card.component.html',
  styleUrls: ['./p-card.component.css']
})
export class PCardComponent {
  @Input()
  product!: Product;
  
  @Output() addToCart = new EventEmitter<Product>();

  onAddToCart() {
    this.addToCart.emit(this.product);
  }
}
