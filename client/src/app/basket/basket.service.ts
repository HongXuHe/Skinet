import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, BasketTotals, IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket.model';
import { IProduct } from '../shared/models/product.model';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl =environment.apiUrl;
  constructor(private http:HttpClient) { }

  private basketSource =new BehaviorSubject<IBasket>(new Basket);
  basket$ =this.basketSource.asObservable();

  private basketTotalSource =new BehaviorSubject<IBasketTotals>(new BasketTotals);
  basketTotal$=this.basketTotalSource.asObservable();

  getBasket(id:string):Observable<IBasket>{
   return this.http.get<IBasket>(`${this.baseUrl}basket/${id}`)
    .pipe(map(bas=>{this.basketSource.next(bas);
      this.calculateTotals();
          return bas;}));
  }

  setBasket(basket:IBasket){
    return this.http.post<IBasket>(`${this.baseUrl}basket`,basket)
    .subscribe(res =>{
       this.basketSource.next(res)
       this.calculateTotals();
     // console.log(res);
      },
    error =>{
      console.log(error);
    })
  }

  deleteBasket(basketId:string){
    return this.http.delete(`${this.baseUrl}basket/${basketId}`)
    .subscribe(()=>{
      this.basketSource.next(new Basket);
      this.basketTotalSource.next(new BasketTotals);
      localStorage.removeItem("basket_id")
    })
  }

  removeBasketItem(itemId:number){
    let basket =this.getCurrentBasketValue();
    let itemIndex =basket.items.findIndex(i=>i.id===itemId);
    basket.items =basket.items.filter(x=>x.id !==itemId);
    //   basket.items.forEach((item,index)=>{
    //     if(index==itemIndex) basket.items.splice(itemIndex,1);
    //  });
     if(basket.items.length>0){
      this.setBasket(basket);
     }else{
      this.deleteBasket(basket.id);
     }


  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  addItemToBasket(item:IProduct,quantity=1){
    const itemToAdd:IBasketItem =this.mapProductItemToBasketItem(item,quantity);
    let basket =this.getCurrentBasketValue();
    if(basket.items.length ===0){
     basket =  this.createBasket();
    }

    basket.items =this.addOrUpdateItem(basket.items,itemToAdd,quantity);
    this.setBasket(basket);
  }
   mapBasketItemToProductItem(item:IBasketItem):IProduct{
    const product:IProduct ={
      id:item.id,
       name :item.productName,
       price:item.price,
       pictureUrl:item.pictureUrl,
      description:'',
      productBrand:item.brand,
      productType:item.type
    }

    return product;
  }

 private mapProductItemToBasketItem(item:IProduct,quantity=1){
    const basketItem:IBasketItem ={
      id:item.id,
       productName :item.name,
       price:item.price,
       pictureUrl:item.pictureUrl,
       quantity:quantity,
      brand:item.productBrand,
      type:item.productType
    }

    return basketItem;
  }

  private createBasket():IBasket{
    const basket = new Basket();
    localStorage.setItem('basket_id',basket.id);
    return basket;
  }

  private addOrUpdateItem(basketItems:IBasketItem[],newItem:IBasketItem,quantity:number)
  :IBasketItem[]{
   let existItemsIndex = basketItems.findIndex(p=>p.id===newItem.id);
   if(existItemsIndex >=0){
    basketItems[existItemsIndex].quantity+=quantity
    if(basketItems[existItemsIndex].quantity <=0){

     basketItems.forEach((item,index)=>{
        if(index==existItemsIndex) basketItems.splice(existItemsIndex,1);
     });

    }
   }else{
     newItem.quantity =quantity;
    basketItems.push(newItem);
   }
      return basketItems;
  }

  private calculateTotals(){
    const basket =this.getCurrentBasketValue();
    const shipping =0;
    const subtotal =basket.items.reduce((sum,current)=>(current.price*current.quantity) +sum,0);
    const total =subtotal+shipping;
    this.basketTotalSource.next({shipping,subTotal:subtotal,total});
  }
}
