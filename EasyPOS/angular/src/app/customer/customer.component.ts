import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { CustomerService, CustomerDto } from '@proxy/customer';
import { customerStatusOptions } from '@proxy/enums';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss'],
  providers: [ListService]
})
export class CustomerComponent implements OnInit {
  customer = { items: [], totalCount: 0 } as PagedResultDto<CustomerDto>;
  selectedCustomer = {} as CustomerDto; // declare selectedBook
  form: FormGroup; // add this line
  isModalOpen = false; // add this line
  customerStatusTypes = customerStatusOptions;

  constructor(
    public readonly list: ListService,
    private customerService: CustomerService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) { }

  ngOnInit(): void {
    const customerStreamCreator = (query) => this.customerService.getList(query);

    this.list.hookToQuery(customerStreamCreator).subscribe((response) => {
      this.customer = response;
    })
  }

  createCustomer() {
    this.selectedCustomer = {} as CustomerDto; 
    this.buildForm();
    this.isModalOpen = true;
  }

  editCustomer(id: string) {
    this.customerService.get(id).subscribe((customer) => {
      this.selectedCustomer = customer;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      idNumber: ['', Validators.required],
      rtn: ['', Validators.required],
      address: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      code: ['', Validators.required],
      //status: [null, Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedCustomer.id
    ? this.customerService.update(this.selectedCustomer.id, this.form.value)
    : this.customerService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.customerService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
}
