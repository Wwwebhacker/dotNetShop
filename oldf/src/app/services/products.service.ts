import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Product } from '../models/product.model';
import {Env} from '../env' ;
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {


  

  constructor(private http: HttpClient, private authService: AuthService) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(Env.baseUrl + '/api/Items');
  }

  getProduct(id: number): Observable<Product>{
    return this.http.get<Product>(Env.baseUrl + `/api/Items/${id}`);
  }

  addProduct(product: Product){
    return this.http.post(Env.baseUrl +`/api/Items`, product);
  }

  updateProduct(product: Product){
    return this.http.put(Env.baseUrl +`/api/Items/${product.id}`, product);
  }
}
