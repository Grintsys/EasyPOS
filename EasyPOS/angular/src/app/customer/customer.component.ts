import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { CustomerService } from '@proxy/app-services';
import { CustomerDto } from '@proxy/dtos';
import { customerStatusOptions } from '@proxy/enums';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss'],
  providers: [ListService]
})
export class CustomerComponent implements OnInit {
  customer = { items: [], totalCount: 0 } as PagedResultDto<CustomerDto>;
  form: FormGroup; // add this line
  isModalOpen = false; // add this line
  customerStatusTypes = customerStatusOptions;
  
  constructor(
    public readonly list: ListService, 
    private customerService: CustomerService,
    private fb: FormBuilder
    ) { }

  ngOnInit(): void {
    const customerStreamCreator = (query) => this.customerService.getList(query);

    this.list.hookToQuery(customerStreamCreator).subscribe((response) => {
      this.customer = response;
    })
  }

  createCustomer() {
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm(){
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

  save(){
    if(this.form.invalid){
      return;
    }

    this.customerService.create(this.form.value).subscribe(() =>{
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    })
  }
}
