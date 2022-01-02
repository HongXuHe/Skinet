import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasketTotals } from '../shared/models/basket.model';
import { IProduct } from '../shared/models/product.model';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
basket$?: Observable<IBasket>
basket?:IBasket;
  constructor(private basketService:BasketService) { }

  ngOnInit(): void {

      this.basket$ =this.basketService.basket$;
      const basketId=localStorage.getItem('basket_id');
      if(basketId){
        this.basketService.getBasket(basketId)
        .subscribe(x=>this.basket =x);
      }

  }
  removeBasketItem(itemId:number){
    this.basketService.removeBasketItem(itemId);
  }
  onQuantityMinus(itemId:number){
   let product =this.getProductById(itemId);
    if(product){
      this.basketService.addItemToBasket(product,-1)
    }

  }
  onQuantityPlus(itemId:number){
    let product =this.getProductById(itemId);
    if(product){
      this.basketService.addItemToBasket(product,1)
    }
  }
  private getProductById(itemId:number):IProduct|null{
    let index =this.basket?.items.findIndex(i=>i.id===itemId);
   if(index !=undefined){
    let basketItem =this.basket?.items[index];
    if(basketItem){
      return this.basketService.mapBasketItemToProductItem(basketItem);
    }
   }
   return null;
  }
}
