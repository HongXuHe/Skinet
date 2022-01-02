import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { IBasket } from './shared/models/basket.model';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Skinet';
  constructor(private basketService:BasketService){
  }

  ngOnInit(): void {
    const basketId =localStorage.getItem('basket_id');
    if(basketId){
      this.basketService.getBasket(basketId)
      .subscribe((res)=>{
        console.log(res)
      },error=>{
        console.log(error);
      })
    }
  }
}
