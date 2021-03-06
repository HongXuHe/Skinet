import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';


const route:Routes =[
  {path:'',component:ShopComponent},
  {path:':id',component:ProductDetailsComponent
,data:{breadcrumb:{alias:'productDetails'}}},
]
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(route)
  ],
  exports:[RouterModule]
})
export class ShopRoutingModule { }
