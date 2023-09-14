import { Component, OnInit } from '@angular/core';
import { Order } from '../models/order.model';
import { OrdersService } from '../services/orders.service';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent{
  orders: Order[] = [];
  intervalId = -1;

  constructor(private orderService: OrdersService) {
  }

  ngOnInit(): void {
    this.load();
    this.intervalId = window.setInterval(() => this.load(), 5000);
  }

  load(){
    this.orderService.getOrders().subscribe((orders) => {
      this.orders = orders;
    });
  }

  ngOnDestroy(){
    clearInterval(this.intervalId);
  }

  getSum(order: Order){
    return order.products.reduce((s,p)=> s + p.price, 0);
  }
}
