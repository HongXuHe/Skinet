import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product.model';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
 product:IProduct | null | undefined;
 productId:number=0;
 quantity=1;
  constructor(private shopService:ShopService,
    private basketService:BasketService,
     private route:ActivatedRoute,
     private bcService:BreadcrumbService) {
       this.bcService.set('@productDetails','');
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(par=>{
      let id =par.get('id');
       if(id){
        this.productId =+id;
       }
    });
    this.loadProduct();
  }

  loadProduct(){
    this.shopService.getProduct(this.productId)
    .subscribe(p=>{this.product =p,
      this.bcService.set('@productDetails',p.name);},
      error =>{
        console.log(error);
      });
  }

  addProductToCard(){
    if(this.product && this.quantity>0){
      this.basketService.addItemToBasket(this.product,this.quantity);
    }
  }

  increaseQuantity(){
    this.quantity++;
  }
  decreaseQuantity(){
    if(this.quantity >1){
      this.quantity--;
    }

  }

}
