import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brands.model';
import { ProductFilter } from '../shared/models/product-filter.model';
import { IType } from '../shared/models/product-type.model';
import { IProduct } from '../shared/models/product.model';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
@ViewChild('search', { static: true }) search: ElementRef | undefined;
products:IProduct[] =[];
types:IType[] =[];
brands:IBrand[] =[];
totalCount =0;
shopParams =new ProductFilter();


sortOptions =[
  {name: 'Alphabetical', value:'name'},
  {name:'Price: Low to High', value: 'priceAsc'},
  {name: 'Price: High to Low', value:'priceDesc'}
];
  constructor(private shopServe:ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopServe.getProducts(this.shopParams)
    .subscribe(res=>{
      this.products =res.data;
      this.shopParams.pageNumber=res.pageIndex;
      this.shopParams.pageSize =res.pageSize;
      this.totalCount =res.count;
    },
    error =>{
      console.log(error);
    })
  }

  getBrands(){
    this.shopServe.getBrands()
    .subscribe(res=>{
      this.brands =[{id:0,name:'All'},...res];
    },
    error =>{
      console.log(error);
    })
  }

  getTypes(){
    this.shopServe.getTypes()
    .subscribe(res=>{
      this.types =[{id:0,name:'All'},...res];
    },
    error =>{
      console.log(error);
    })
  }

  //on brand selected
  onBrandSelected(brandId:number){
    this.shopParams.brandId =brandId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  //on type selected

  onTypeSelected(typeId:number){
    this.shopParams.typeId =typeId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onSortSelected(selected:string){
    this.shopParams.sort =selected;
    this.getProducts();
  }

  onPageChanged(pageValue:number){
    if(this.shopParams.pageNumber !==pageValue){
      this.shopParams.pageNumber =pageValue;
      this.getProducts();
    }
  }

  onSearchClick(){
   this.shopParams.search = this.search?.nativeElement.value;
   this.shopParams.pageNumber=1;
   this.getProducts();
  }

  onResetClick(){
    this.shopParams = new ProductFilter();
    if(this.search){
      this.search.nativeElement.value =null;
    }
    this.getProducts();
  }
}
