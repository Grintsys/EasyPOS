import { Component,  OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { fuseAnimations } from '@fuse/animations';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';
import { Customer } from './customer.model';
import { CustomerService } from './customer.service';


@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss'],
  animations   : fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class CustomerComponent implements OnInit, OnDestroy {

    customer: Customer;
    pageType: string;
    customerForm: FormGroup;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
    * Constructor
    *
    * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
    * @param {FuseTranslationLoaderService} _customerService
    * @param {FormBuilder} _formBuilder
   */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _customerService: CustomerService,
        private _formBuilder: FormBuilder,
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        // Set the default
        this.customer = new Customer();

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }


    /**
     * On init
     */
    ngOnInit(): void
    {
        // Subscribe to update customer on changes
        this._customerService.onCustomerChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(customer => {

                if ( customer )
                {
                    this.pageType = 'edit';
                }
                else
                {
                    this.pageType = 'new';
                }
                this.customerForm = this.createcustomerForm();
            });
    }


    /**
    * On destroy
    */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Create customer form
     *
     * @returns {FormGroup}
    */
    createcustomerForm(): FormGroup
    {
    return this._formBuilder.group({
        firstName   : [this.customer.firstName],
        lastName    : [this.customer.lastName],
        identity    : [this.customer.identity],
        rtn         : [this.customer.rtn],
        address     : [this.customer.address],
        phone       : [this.customer.phone],
        status      : [this.customer.status],
    });
    }

}

