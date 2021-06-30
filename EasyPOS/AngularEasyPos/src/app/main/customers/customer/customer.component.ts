import { Component, OnDestroy, OnInit, ViewEncapsulation } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";

import { fuseAnimations } from "@fuse/animations";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";

import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";
import { CreateUpdateCustomerDto, CustomerDto } from "../customer.model";
import { CustomerService } from "../customer.service";
import { ActivatedRoute, Router } from "@angular/router";
import { FuseUtils } from "@fuse/utils";

@Component({
    selector: "app-customer",
    templateUrl: "./customer.component.html",
    styleUrls: ["./customer.component.scss"],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None,
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

    ngOnInit(): void {
        // Subscribe to update customer on changes
        this._customerService.onCustomerChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data.Type == "view") {
                    this.getCustomerById(data.Id);
                    this.pageType = "view";
                } else if (data.Type == "edit") {
                    this.getCustomerById(data.Id);
                    this.pageType = "edit";
                } else {
                    this.pageType = "new";
                    var suffix = FuseUtils.generateGUID();
                    this.customer.code = 'CEP'+suffix.substring(0,5);
                }
                this.customerForm = this.createcustomerForm(this.pageType);
            });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    createcustomerForm(type: string): FormGroup {
        return this._formBuilder.group({
            fullName: new FormControl(
                { value: this.customer.fullName, disabled: type === "view" },
                Validators.compose([
                    Validators.required,
                    Validators.minLength(1),
                ])
            ),
            idNumber: new FormControl(
                { value: this.customer.idNumber, disabled: type === "view" },
                Validators.compose([
                    Validators.required,
                    Validators.minLength(13),
                    Validators.maxLength(13),
                    Validators.pattern(/^[0-9]\d*$/)
                ])
            ),
            rtn: new FormControl(
                { value: this.customer.rtn, disabled: type === "view" },
                Validators.compose([
                    Validators.required,
                    Validators.minLength(14),
                    Validators.maxLength(14),
                    Validators.pattern(/^[0-9]\d*$/),
                ])
            ),
            address: new FormControl(
                { value: this.customer.address, disabled: type === "view"},  
            ),
            phoneNumber: new FormControl({
                value: this.customer.phoneNumber, disabled: type === "view"},
                Validators.compose([Validators.pattern(/^\([0-9]{3}[\-\)][0-9]{4}-[0-9]{4}$/)])  
            ),
            status: new FormControl({
                value: this.customer.status,
                disabled: type === "view",
            }),
            code: new FormControl(
                { value: this.customer.code, disabled: true }
            ),
        });
    }

    addcustomer() {
        let newCustomer = this.getFormObject();
        this._customerService.create(newCustomer).then(
            () => {
                this.router.navigate(["/customer-list"]);
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }

    getFormObject(): CreateUpdateCustomerDto {
        let newCustomer = new CreateUpdateCustomerDto();
        newCustomer.fullName =
            this.customerForm.get("fullName").value == undefined
                ? this.customer.fullName
                : this.customerForm.get("fullName").value;
        newCustomer.idNumber =
            this.customerForm.get("idNumber").value == undefined
                ? this.customer.idNumber
                : this.customerForm.get("idNumber").value;
        newCustomer.rtn =
            this.customerForm.get("rtn").value == undefined
                ? this.customer.rtn
                : this.customerForm.get("rtn").value;
        newCustomer.address =
            this.customerForm.get("address").value == undefined
                ? this.customer.address
                : this.customerForm.get("address").value;
        newCustomer.phoneNumber =
            this.customerForm.get("phoneNumber").value == undefined
                ? this.customer.phoneNumber
                : this.customerForm.get("phoneNumber").value;
        newCustomer.status =
            this.customerForm.get("status").value == undefined
                ? this.customer.status
                : this.customerForm.get("status").value;
        newCustomer.code =
            this.customerForm.get("code").value == undefined
                ? this.customer.code
                : this.customerForm.get("code").value;
        return newCustomer;
    }

    savecustomer() {
        let selectedCustomer = this.getFormObject();
        this._customerService.update(this.customer.id, selectedCustomer).then(
            () => {
                this.router.navigate(["/customer-list"]);
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
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
