import * as uuid from 'uuid';

export interface IBasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}

export interface IBasket {
  id: string;
  items: IBasketItem[];
}

export class Basket implements IBasket{
  id: string =uuid.v4();
  items: IBasketItem[] =[];

}

export interface IBasketTotals
{
  shipping:number;
  subTotal:number;
  total:number;
}
export class BasketTotals implements IBasketTotals{
  shipping: number =0;
  subTotal: number=0;
  total: number=0;

}
