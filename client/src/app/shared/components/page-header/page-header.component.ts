import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent implements OnInit {
@Input('pageNumber') pageNumber:number =0;
@Input('pageSize') pageSize:number =0;
@Input('totalCount') totalCount:number =0;
  constructor() { }

  ngOnInit(): void {
  }

}
