import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-page-footer',
  templateUrl: './page-footer.component.html',
  styleUrls: ['./page-footer.component.scss']
})
export class PageFooterComponent implements OnInit {
@Input('totalCount') totalCount =0;
@Input ('pageSize') pageSize =0;
@Output() nextPage = new EventEmitter<number>();
  constructor() { }

  ngOnInit(): void {
  }
  onPageChanged(pageValue:{page:number}){
    this.nextPage.emit(pageValue.page);
  }
}
