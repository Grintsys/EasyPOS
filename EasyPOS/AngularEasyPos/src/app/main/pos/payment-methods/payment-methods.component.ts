import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { PaymentMethodDto, SavePaymentMethod } from 'app/main/orders/order.model';

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

    paymentMethodDto: PaymentMethodDto;

    sideBarPaymentMethod: SavePaymentMethod;

    paymentMethodList: Array<PaymentMethodTemp>;
    selectedPaymentMethod: PaymentMethodTemp;
    paymentMethodEnum = PaymentMethodEnumTemp;

    public form: FormGroup = new FormGroup({
        userName: new FormControl(''),
    });
    amount: number;

    orderSubtotal: number;
    orderTaxes: number;
    orderDiscount: number;

    total: number;
    bankName: string;
    date: Date;
    account: string;
    reference: string;
    clientName: string;
    clientId: string;
    certificateNumber: string;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        public matDialogRef: MatDialogRef<PaymentMethodsComponent>,
        public _posService: PosService,
        @Inject(MAT_DIALOG_DATA) private _data: any
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.paymentMethodDto = _data.PaymentMethod;
        this.sideBarPaymentMethod = new SavePaymentMethod;
        this.orderSubtotal = _data.subtotal;
        this.orderTaxes = _data.taxes;
        this.orderDiscount = _data.discount;
        this.amount = 0;
        this.initialDataTemp();
    }

    ngOnInit(): void {
    }

    totalChange(event) {
        this.total = parseFloat(event);
    }

    bankNameChange(event) {
        this.bankName = event;
    }

    dateChange(event) {
        this.date = event;
    }

    accountChange(event) {
        this.account = event;
    }

    referenceChange(event) {
        this.reference = event;
    }

    clientNameChange(event) {
        this.clientName = event;
    }

    clientIdChange(event) {
        this.clientId = event;
    }

    certificateNumberChange(event) {
        this.certificateNumber = event;
    }

    initialDataTemp(): void {
        this.paymentMethodList = [
            new PaymentMethodTemp('CASH', 'Efectivo', 'cash', true),
            new PaymentMethodTemp('TRANSFER', 'Transferencia', 'transfer', false),
            new PaymentMethodTemp('CREDITCARD', 'Tarjeta', 'credit-card', false),
            new PaymentMethodTemp('CHECK', 'Cheque', 'check', false),
        ];
        this.selectedPaymentMethod = this.paymentMethodList[0];
    }

    setSelectedPaymentMethod(_methodType: string): void {
        (this.paymentMethodList.find(el => el.isSelected === true)).isSelected = false;
        this.selectedPaymentMethod = this.paymentMethodList.find(el => el.methodType === _methodType);
        this.selectedPaymentMethod.isSelected = true;
    }

    isVisible(): boolean {
        this.selectedPaymentMethod
        return true;
    }

    save() {
        if (this.selectedPaymentMethod.methodType == 'CASH') {
            if (this.total != undefined) {
                this.sideBarPaymentMethod.type = 'CASH';
                this.sideBarPaymentMethod.cash.total = this.total;
                this.matDialogRef.close(this.sideBarPaymentMethod);
            }
        }
        else if (this.selectedPaymentMethod.methodType == 'TRANSFER') {
            this.sideBarPaymentMethod.type = 'TRANSFER';
            this.sideBarPaymentMethod.transfer.total = this.total;
            this.sideBarPaymentMethod.transfer.account = this.account;
            this.sideBarPaymentMethod.transfer.dateTime = this.date;
            this.sideBarPaymentMethod.transfer.reference = this.reference;
            this.matDialogRef.close(this.sideBarPaymentMethod);
        }
        else if (this.selectedPaymentMethod.methodType == 'CREDITCARD') {
            this.sideBarPaymentMethod.type = 'CREDITCARD';
            this.sideBarPaymentMethod.card.total = this.total;
            this.sideBarPaymentMethod.card.name = this.clientName;
            this.sideBarPaymentMethod.card.validThru = this.date;
            this.sideBarPaymentMethod.card.personId = this.clientId;
            this.matDialogRef.close(this.sideBarPaymentMethod);
        }
        else if (this.selectedPaymentMethod.methodType == 'CHECK') {
            this.sideBarPaymentMethod.type = 'CHECK';
            this.sideBarPaymentMethod.bank.total = this.total;
            this.sideBarPaymentMethod.bank.bank = this.clientName;
            this.sideBarPaymentMethod.bank.date = this.date;
            this.matDialogRef.close(this.sideBarPaymentMethod);
        }
    }
}

export class PaymentMethodTemp {
    methodType: string;
    title: string;
    icon: string;
    isSelected: boolean;

    constructor(_methodType: string, _title: string, _icon: string, _isSelected: boolean) {
        this.methodType = _methodType;
        this.icon = _icon;
        this.title = _title;
        this.isSelected = _isSelected;
    }
}

enum PaymentMethodEnumTemp {
    CASH = "CASH",
    TRANSFER = "TRANSFER",
    CREDITCARD = "CREDITCARD",
    CHECK = "CHECK",
}
