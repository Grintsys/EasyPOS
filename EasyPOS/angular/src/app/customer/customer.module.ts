import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';


@NgModule({
  declarations: [CustomerComponent],
  imports: [
    SharedModule,
    CustomerRoutingModule
  ]
})
export class CustomerModule { }
