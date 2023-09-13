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
  selectedFile: File | undefined;
  id?: number;
  productForm = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    price: [0.0, Validators.required],
    inventoryCount: [0, [Validators.required, Validators.pattern('^[0-9]*$')]]
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
      this.id = product.id;
      this.productForm.setValue({name: product.name, description: product.description, price: product.price, inventoryCount: product.inventoryCount})
    });
  }


  onFileSelected(event: any) {
    this.selectedFile = <File>event.target.files[0];
  }

  onSubmit() {
    if(!this.productForm.valid){
      return;
    }
    const product: Product = {
      name: this.productForm.value.name || '', 
      description: this.productForm.value.description || '',
      price: this.productForm.value.price || 0.0,
      inventoryCount: this.productForm.value.inventoryCount || 0
    }
    if(!this.id){
      if(!this.selectedFile){
        return;
      }
      
      console.log(product);

      this.service.addProduct(product, this.selectedFile).subscribe(responce => {
        this.redirect();
      }); 
    }else{
      product.id = this.id;
      this.service.updateProduct(product).subscribe(response => {
        this.redirect();
      })
    }    
  }

  redirect(){
    this.router.navigate(['products']);
  }
}
