import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { BankDto, CreateUpdateBankCheckDto, CreateUpdatePaymentMethodDto, PaymentMethodDto } from 'app/main/orders/order.model';

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
    paymentMethodsData: CreateUpdatePaymentMethodDto;
    bankList: BankDto[];

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
        this.paymentMethodsData = _data.paymentMethdosData;
        this.orderSubtotal = _data.subtotal;
        this.orderTaxes = _data.taxes;
        this.orderDiscount = _data.discount;
        this.amount = 0;
        this.bankList = [];
        this.initialDataTemp();
    }

    getConfigList(filter: string) {
        this._posService.getConfList(filter).then(
            (d) => {
                this.bankList = JSON.parse(d[0].value);
                console.log(this.bankList);
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }

    ngOnInit(): void {
        this.getConfigList('Bancos');
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
                this.paymentMethodsData.cash.total = this.total;
                this.matDialogRef.close(this.paymentMethodsData);
            }
        }
        else if (this.selectedPaymentMethod.methodType == 'TRANSFER') {
            this.paymentMethodsData.wireTransfer.total = this.total;
            this.paymentMethodsData.wireTransfer.account = this.account;
            this.paymentMethodsData.wireTransfer.dateTime = this.date;
            this.paymentMethodsData.wireTransfer.reference = this.reference;
            this.matDialogRef.close(this.paymentMethodsData);
        }
        else if (this.selectedPaymentMethod.methodType == 'CREDITCARD') {
            this.paymentMethodsData.creditDebitCard.total = this.total;
            this.paymentMethodsData.creditDebitCard.name = this.clientName;
            this.paymentMethodsData.creditDebitCard.validThru = this.date;
            this.paymentMethodsData.creditDebitCard.personId = this.clientId;
            this.matDialogRef.close(this.paymentMethodsData);
        }
        else if (this.selectedPaymentMethod.methodType == 'CHECK') {
            var bankCheck = new CreateUpdateBankCheckDto;
            bankCheck.total = this.total;
            bankCheck.bank = this.bankName;
            bankCheck.date = this.date;

            this.paymentMethodsData.bankChecks.push(bankCheck);
            this.matDialogRef.close(this.paymentMethodsData);
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
