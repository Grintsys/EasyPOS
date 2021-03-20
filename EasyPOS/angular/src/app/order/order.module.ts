import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { OrderRoutingModule } from './order-routing.module';
import { OrderComponent } from './order.component';


@NgModule({
  declarations: [OrderComponent],
  imports: [
    SharedModule,
    OrderRoutingModule
  ]
})
export class OrderModule { }
