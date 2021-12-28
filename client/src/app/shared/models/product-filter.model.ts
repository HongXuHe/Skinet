export class ProductFilter
{
  typeId:number|null =0;
  brandId:number|null =0;
  sort:string ='name';
  pageNumber =1;
  pageSize=6;
  search:string|null=null;
}
