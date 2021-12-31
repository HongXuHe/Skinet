import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './components/product-item/product-item.component';
import { ShopService } from './shop.service';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { RouterModule } from '@angular/router';
import { ShopRoutingModule } from './shop-routing.module';



@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent,
    ProductDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule
  ],
  exports:[],
  providers:[]
})
export class ShopModule { }
