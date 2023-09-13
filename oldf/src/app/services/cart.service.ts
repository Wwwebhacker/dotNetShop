import { EventEmitter, Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import {Env} from '../env' ;
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cartUpdateEvent: EventEmitter<Product[]> = new EventEmitter<Product[]>();
  private products: Product[] = [];

  constructor(private http: HttpClient) { }


  addToCart(product: Product){
    if (!this.products.some(p => p.id === product.id)) {
      this.products.push(product);
    }
    this.emitUpdate();
  }

  removeFrom(product: Product){
    this.products = this.products.filter(p => p.id !== product.id);
    this.emitUpdate();
  }

  emitUpdate(){
    this.cartUpdateEvent.emit(this.products);
  }
  subscribeToProducts(callback: (products: Product[]) => void){
    this.cartUpdateEvent.subscribe(callback);
    this.emitUpdate();
  }

  buy(){
    return this.http.post(Env.baseUrl + `/api/Orders`, this.products.map(p => p.id));
  }
}
