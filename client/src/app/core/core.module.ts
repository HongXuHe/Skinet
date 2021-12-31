import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotFountComponent } from './not-fount/not-fount.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { SectionHeaderComponent } from './components/section-header/section-header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';



@NgModule({
  declarations: [
    NavBarComponent,
    TestErrorComponent,
    NotFountComponent,
    ServerErrorComponent,
    SectionHeaderComponent
  ],
  imports: [
    CommonModule,
    BreadcrumbModule,
    RouterModule
  ],
  exports:[NavBarComponent,SectionHeaderComponent]
})
export class CoreModule { }
