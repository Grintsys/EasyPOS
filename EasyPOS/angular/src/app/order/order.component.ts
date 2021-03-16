import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { OrderService, OrderDto } from '@proxy/order';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { orderStatesOptions } from '@proxy/enums/order-states.enum';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
  providers: [ListService]
})
export class OrderComponent implements OnInit {
  order = { items: [], totalCount: 0 } as PagedResultDto<OrderDto>;
  form: FormGroup; // add this line
  isModalOpen = false; // add this line
  orderStatusTypes = orderStatesOptions;

  constructor(
    public readonly list: ListService,
    private orderService: OrderService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    const orderStreamCreator = (query) => this.orderService.getList(query);

    this.list.hookToQuery(orderStreamCreator).subscribe((response) => {
      this.order = response;
    })
  }

  buildForm() {
    this.form = this.fb.group({
      order: [null, Validators.required],
    });
  }

  createCustomer() {
    this.buildForm();
    this.isModalOpen = true;
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    this.orderService.create(this.form.value).subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    })
  }

}
