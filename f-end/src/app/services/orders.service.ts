import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Env } from '../env';
import { Order } from '../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private http: HttpClient) { }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(Env.baseUrl + '/api/Orders');
  }

  // getProduct(id: number): Observable<Product>{
  //   return this.http.get<Product>(Env.baseUrl + `/api/Orders/${id}`);
  // }

}
