import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NavigationExtras, Router } from "@angular/router";
import { catchError, Observable, throwError } from "rxjs";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor
{
  constructor(private router:Router) {
  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
  {
    return next.handle(req)
    .pipe(catchError(error=>{
      if(error){
        if(error.status ===404){
          console.log(error);
          this.router.navigateByUrl('/not-found');
        }
        if(error.status ===500){
          let navigationExtras:NavigationExtras ={state:{error:error.error}};
          console.log('matt' +error.error);
          this.router.navigateByUrl('/server-error',navigationExtras);
        }
      }
      return throwError(error)
    }))
  }

}
