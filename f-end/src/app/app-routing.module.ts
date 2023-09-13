import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductsComponent } from './products/products.component';
import { ProductsEditComponent } from './products-edit/products-edit.component';
import { CartListComponent } from './cart-list/cart-list.component';
import { AuthComponent } from './auth/auth.component';
import { AccountDetailsComponent } from './account-details/account-details.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'products', component: ProductsComponent},
  {path: 'products/edit', component: ProductsEditComponent},
  {path: 'cart', component: CartListComponent},
  {path: 'auth', component: AuthComponent},
  {path: 'account', component: AccountDetailsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
