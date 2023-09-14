import { Injectable } from '@angular/core';
import { EMPTY, Observable, map } from 'rxjs';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Product } from '../models/product.model';
import {Env} from '../env' ;

@Injectable({
  providedIn: 'root'
})
export class ProductsService {


  

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(Env.baseUrl + '/api/Products').pipe(
      map(ps => {ps.forEach(
        p => {
          p.imageUrl = Env.baseUrl + '/api/Products/Files/'+ p.imageUrl?.replace("/images/", "")
        }
      )
      return ps
    })
    );
  }

  getProduct(id: number): Observable<Product>{
    return this.http.get<Product>(Env.baseUrl + `/api/Products/${id}`);
  }

  addProduct(product: Product, file: File){
    const formData: FormData = new FormData();
    formData.append('Name', product.name);
    formData.append('Description', product.description);
    formData.append('Price',  product.price.toString());
    formData.append('inventoryCount',  product.inventoryCount.toString());
    formData.append('Image', file, file.name);

    return this.http.post(Env.baseUrl +`/api/Products`, formData);
  }

  updateProduct(product: Product, file?: File){
   
    

    const formData: FormData = new FormData();
    formData.append('Name', product.name);
    formData.append('Id', product.id?.toString() || '');
    formData.append('Description', product.description);
    formData.append('Price',  product.price.toString());
    formData.append('inventoryCount',  product.inventoryCount.toString());
    if (file){
      formData.append('Image', file, file.name);
    }

    return this.http.put(Env.baseUrl +`/api/Products/${product.id}`, formData);
  }

  deleteProduct(id: number){
    return this.http.delete(Env.baseUrl +`/api/Products/${id}`);
  }

  // private createImageFromBlob(image: Blob) {
  //   let reader = new FileReader();
  //   reader.addEventListener("load", () => {
  //     this.imageToShow = reader.result;
  //   }, false);

  //   if (image) {
  //     reader.readAsDataURL(image);
  //   }
  // }
}
