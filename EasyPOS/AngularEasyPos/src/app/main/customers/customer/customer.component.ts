import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

import { fuseAnimations } from '@fuse/animations';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';
import { CreateUpdateCustomerDto, CustomerDto } from '../customer.model';
import { CustomerService } from '../customer.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
    selector: 'app-customer',
    templateUrl: './customer.component.html',
    styleUrls: ['./customer.component.scss'],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None
})
export class CustomerComponent implements OnInit, OnDestroy {
    customer: CustomerDto;
    pageType: string;
    customerForm: FormGroup;

    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _customerService: CustomerService,
        private _formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        // Set the default
        this.customer = new CustomerDto();
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    /**
     * On init
     */
    ngOnInit(): void {
        // Subscribe to update customer on changes
        this._customerService.onCustomerChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(data => {
                if (data.Type == 'view') {
                    this.getCustomerById(data.Id);
                    this.pageType = 'view';
                    this.customerForm = this.createcustomerForm(this.pageType);
                }
                else if (data.Type == 'edit') {
                    this.getCustomerById(data.Id);
                    this.pageType = 'edit';
                    this.customerForm = this.createcustomerForm(this.pageType);
                }
                else {
                    this.pageType = 'new';
                    this.customerForm = this.createcustomerForm(this.pageType);
                }
                
            });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    createcustomerForm(type: string): FormGroup {
        return this._formBuilder.group({
            firstName: new FormControl({ value: this.customer.firstName, disabled: type === 'view' }),
            lastName: new FormControl({ value: this.customer.lastName, disabled: type === 'view' }),
            idNumber: new FormControl({ value: this.customer.idNumber, disabled: type === 'view' }),
            rtn: new FormControl({ value: this.customer.rtn, disabled: type === 'view' }),
            address: new FormControl({ value: this.customer.address, disabled: type === 'view' }),
            phoneNumber: new FormControl({ value: this.customer.phoneNumber, disabled: type === 'view' }),
            status: new FormControl({ value: this.customer.status, disabled: type === 'view' }),
            code: new FormControl({ value: this.customer.code, disabled: type === 'view' }),
        });
    }

    addcustomer(){
        let newCustomer = this.getFormObject();
        this._customerService.create(newCustomer).then(
            () => {
                this.router.navigate(['/customer-list']);
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            });
    }

    getFormObject(): CreateUpdateCustomerDto{
        let newCustomer = new CreateUpdateCustomerDto();
        newCustomer.firstName = this.customerForm.get('firstName').value;
        newCustomer.lastName = this.customerForm.get('lastName').value;
        newCustomer.idNumber = this.customerForm.get('idNumber').value;
        newCustomer.rtn = this.customerForm.get('rtn').value;
        newCustomer.address = this.customerForm.get('address').value;
        newCustomer.phoneNumber = this.customerForm.get('phoneNumber').value;
        newCustomer.status = this.customerForm.get('status').value;
        newCustomer.code = this.customerForm.get('code').value;
        return newCustomer;
    }
    
    savecustomer(){
        let selectedCustomer = this.getFormObject();
        this._customerService.update(this.customer.id, selectedCustomer).then(
            () => {
                this.router.navigate(['/customer-list']);
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            });
    }

    getCustomerById(id: string) {
        this._customerService.get(id).then(
            (customer) => {
                this.customer = customer;
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }

}

