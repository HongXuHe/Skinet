import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product.model';
import { ShopService } from '../../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
 product:IProduct | null | undefined;
 productId:number=0;
  constructor(private shopService:ShopService, private route:ActivatedRoute) {
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
    .subscribe(p=>this.product =p,
      error =>{
        console.log(error);
      });
  }
}
