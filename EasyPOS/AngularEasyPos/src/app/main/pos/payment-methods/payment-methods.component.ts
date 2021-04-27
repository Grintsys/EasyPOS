import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { PaymentMethodDto, PaymentMethodTypeDto } from 'app/main/orders/order.model';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';
import { PosService } from '../pos.service';

@Component({
    selector: 'payment-methods',
    templateUrl: './payment-methods.component.html',
    styleUrls: ['./payment-methods.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class PaymentMethodsComponent implements OnInit {
    centered = false;
    disabled = false;
    unbounded = false;

    radius: number;
    color: string;

    selectedPaymentMethodType: PaymentMethodTypeDto;
    paymentMethodTypes: PaymentMethodTypeDto[];

    paymentMethodDto: PaymentMethodDto;

    public form: FormGroup = new FormGroup({
        userName: new FormControl(''),
    });
    amount: number;
    subtotal: number;
    taxes: number;
    discount: number;

    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     * @param {MatDialogRef<MailNgrxComposeDialogComponent>} matDialogRef
     * @param _data
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        public matDialogRef: MatDialogRef<PaymentMethodsComponent>,
        public _posService: PosService,
        @Inject(MAT_DIALOG_DATA) private _data: any
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.paymentMethodTypes = [];
        this.selectedPaymentMethodType = new PaymentMethodTypeDto();
        this.paymentMethodDto = _data.PaymentMethod;
        this.subtotal = _data.subtotal;
        this.taxes = _data.taxes;
        this.discount = _data.discount;
        this.amount = 0;
    }

    ngOnInit(): void {
        this.getPaymentMethods();
    }

    getPaymentMethods() {
        this._posService.getPaymentMethods().then(
            (data) => {
                this.paymentMethodTypes = data;
            },
            (error) => {
                console.log("Payment-Methods-Component: Error Getting Payment Methods List " +
                    JSON.stringify(error)
                );
            }
        );
    }

    setSelectedPaymentMethod(id: string) {
        this.selectedPaymentMethodType = this.paymentMethodTypes.find(x => x.id == id);
    }
    change(event) { 
        console.log(event.target.value);
        this.paymentMethodDto.amount = parseInt(event.target.value); 
    }

    addPayment() {
        this.paymentMethodDto.paymentMethodTypeId = this.selectedPaymentMethodType.id;
        this.paymentMethodDto.amount = this.amount;
        this.matDialogRef.close(this.paymentMethodDto);
    }
}
