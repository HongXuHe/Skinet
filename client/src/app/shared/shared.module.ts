import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PageFooterComponent } from './components/page-footer/page-footer.component';
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';



@NgModule({
  declarations: [
    PageFooterComponent,
    PageHeaderComponent,
    OrderTotalsComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot()
  ],
  exports: [PaginationModule,
  PageFooterComponent,
  PageHeaderComponent,
  OrderTotalsComponent,
CarouselModule]
})
export class SharedModule { }
