import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { IBrand } from '../shared/models/brands.model';
import { IPagination } from '../shared/models/pagination.model';
import { ProductFilter } from '../shared/models/product-filter.model';
import { IType } from '../shared/models/product-type.model';
import { IProduct } from '../shared/models/product.model';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
baseUrl ='http://localhost:5000/api/'
  constructor(private http:HttpClient) { }

  //get products
  getProducts(filter:ProductFilter):Observable<IPagination>{
  let params = new HttpParams();
  if(filter.typeId && filter.typeId !==0){
    params =params.append("typeId",filter.typeId)
  }
  if(filter.brandId && filter.brandId !==0){
   params = params.set("brandId",filter.brandId)
  }
  if(filter.search){
    params =params.set('search',filter.search);
  }
  params =params.set('sort',filter.sort);
  params =params.set('pageIndex',filter.pageNumber.toString());
  params =params.set('pageSize',filter.pageSize.toString());
    return this.http.get<IPagination>(`${this.baseUrl}products`,{params:params})
    ;
  }

  //get single product
  getProduct(id:number):Observable<IProduct>{
   return this.http.get<IProduct>(`${this.baseUrl}products/${id}`);
  }

  //get product brands
  getBrands():Observable<IBrand[]>{
    return this.http.get<IBrand[]>(`${this.baseUrl}ProductBrands`);
  }

  //get productTypes
  getTypes():Observable<IType[]>{
    return this.http.get<IBrand[]>(`${this.baseUrl}ProductTypes`);
  }
}
