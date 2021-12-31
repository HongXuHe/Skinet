import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotFountComponent } from './not-fount/not-fount.component';
import { ServerErrorComponent } from './server-error/server-error.component';



@NgModule({
  declarations: [
    NavBarComponent,
    TestErrorComponent,
    NotFountComponent,
    ServerErrorComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[NavBarComponent]
})
export class CoreModule { }
