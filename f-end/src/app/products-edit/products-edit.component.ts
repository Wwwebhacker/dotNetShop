import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsService } from '../services/products.service';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-products-edit',
  templateUrl: './products-edit.component.html',
  styleUrls: ['./products-edit.component.css']
})
export class ProductsEditComponent {
  product?: Product;
  productForm = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required]
  })

  constructor(private route: ActivatedRoute, private router: Router, private fb: FormBuilder, private service: ProductsService){

  }

  ngOnInit(){
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.fetchProduct(params['id']);        
      }
    })
  }

  fetchProduct(id: number){
    this.service.getProduct(id).subscribe(product => {
      this.product = product;
      this.productForm.setValue({name: product.name, description: product.name})
    });
  }

  onSubmit() {
    if(!this.product){
      if(!this.productForm.valid){
        return;
      }
      const product: Product = {name: this.productForm.value.name || '', description: this.productForm.value.description || ''}
      this.service.addProduct(product).subscribe(responce => {
        this.redirect();
      }); 
    }else{
      this.product.name = this.productForm.value.name || '';
      this.product.description = this.productForm.value.description || '';
      this.service.updateProduct(this.product).subscribe(response => {
        this.redirect();
      })
    }    
  }

  redirect(){
    this.router.navigate(['products']);
  }
}
