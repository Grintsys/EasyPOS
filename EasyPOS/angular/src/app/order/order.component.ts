import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { OrderService, OrderDto } from '@proxy/order';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { documentStateOptions } from '@proxy/enums/document-state.enum';
import { CustomerLookupDto, CustomerService } from '@proxy/customer';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

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
  orderStatusTypes = documentStateOptions;
  customers$: Observable<CustomerLookupDto[]>;

  constructor(
    public readonly list: ListService,
    private orderService: OrderService,
    private customerService: CustomerService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) { 
    this.customers$ = customerService.getCustomerLookup().pipe(map((r) => r.items));
  }

  ngOnInit(): void {
    const orderStreamCreator = (query) => this.orderService.getList(query);

    this.list.hookToQuery(orderStreamCreator).subscribe((response) => {
      this.order = response;
    })
  }

  buildForm() {
    this.form = this.fb.group({
      customerId:  [null, Validators.required],
    });
  }

  createOrder() {
    this.buildForm();
    this.isModalOpen = true;
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.orderService.delete(id).subscribe(() => this.list.get());
      }
    });
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
