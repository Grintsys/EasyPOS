import {
    Component,
    Input,
    Output,
    SimpleChanges,
    ViewEncapsulation,
    EventEmitter,
} from "@angular/core";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";

import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { fuseAnimations } from "@fuse/animations";

import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";
import { PaymentMethodsComponent } from "../payment-methods/payment-methods.component";
import { CustomerDialogComponent } from "app/main/customers/customer-dialog/customer-dialog.component";
import {
    CreateUpdateCreditNoteDto,
    CreateUpdateDebitNoteDto,
    CreateUpdateDocumentItemDto,
    CreateUpdateOrderDto,
    CreateUpdatePaymentMethodDto,
    DocumentState,
    OrderDto, OrderItemDto, PaymentMethodDto, PaymentMethodTypeDto,
} from "app/main/orders/order.model";
import { PosService } from "../pos.service";
import { CustomerDto } from "app/main/customers/customer.model";
import { takeUntil } from "rxjs/operators";
import { Subject, Subscription } from "rxjs";
import { Router } from "@angular/router";
import { SharedService } from "app/shared.service";

@Component({
    selector: "pos-sidebar",
    templateUrl: "./pos-sidebar.component.html",
    styleUrls: ["./pos-sidebar.component.scss"],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations,
})
export class PosSidebarComponent {
    centered = false;
    disabled = false;
    unbounded = false;

    radius: number;
    color: string;

    dialogRef: any;

    @Input() order: OrderDto;
    @Output() newOrderEvent = new EventEmitter<OrderDto>();

    customer: CustomerDto;
    isOrderReady: boolean;
    pageType: string;
    
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _matDialog: MatDialog,
        public _posService: PosService,
        private router: Router
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.color = "rgba(223, 196, 0, 0.11)";
        this.order = new OrderDto();
        this.customer = new CustomerDto();
        this.isOrderReady = false;
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        // Subscribe to update customer on changes
        this._posService.onPosChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data.Type == "creditNote") {
                    this.pageType = "Nota de Credito";
                } else if (data.Type == "debitNote") {
                    this.pageType = "Nota de Debito";
                } else {
                    this.pageType = "Orden";
                }
            });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    openPaymentMethodDialog(): void {
        var newPayment: PaymentMethodTypeDto = new PaymentMethodTypeDto();

        const dialogConfig = new MatDialogConfig();
        dialogConfig.panelClass = "payment-method-dialog";
        dialogConfig.data = {
            discount: this.order.discount,
            subtotal: this.order.subTotal,
            taxes: this.order.isv,
            PaymentMethod: newPayment
        };

        this.dialogRef = this._matDialog.open(PaymentMethodsComponent, dialogConfig);

        this.dialogRef.afterClosed().subscribe((response) => {
            if (response != undefined) {
                this.order.paymentMethods.push(response);
                this.order.paymentAmount += response.amount;
            }
            this.validateOrder();
        });
    }

    openDialogToAddCustomer(): void {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.panelClass = "payment-method-dialog";
        dialogConfig.data = {
            isDialog: true
        };

        this.dialogRef = this._matDialog.open(CustomerDialogComponent, dialogConfig);

        this.dialogRef.afterClosed().subscribe((response) => {
            if (response != undefined) {
                this.customer = response;
                this.order.customerId = this.customer.id;
            }
            this.validateOrder();
        });
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes.order.currentValue.items) {
            this.order.subTotal = this.order.items.reduce(function (a, value) {
                return a + value.salePrice * value.quantity;
            }, 0);
            this.order.discount = this.order.items.reduce(function (a, value) {
                return a + (value.salePrice * value.quantity) * value.discount;
            }, 0);
            this.order.isv = this.order.items.reduce(function (a, value) {
                return a + (value.salePrice * value.quantity) * value.taxes;
            }, 0);

            this.order.total = this.order.subTotal + this.order.isv - this.order.discount;
            this.validateOrder();
        }
    }

    validateOrder() {
        if (this.order.customerId != undefined && this.order.items.length > 0
            && this.order.paymentMethods.length > 0) {
            this.isOrderReady = true;
        }
        else if(this.pageType != 'Orden'){
            this.isOrderReady = true;
        }
        else {
            this.isOrderReady = false;
        }
    }

    resetOrder() {
        this.order = new OrderDto();
        this.customer = new CustomerDto();
        
        if(this.pageType != 'Orden'){
            this.pageType = 'Orden';
            this.router.navigate(["/pos"]);
        }
    }

    create(){
        if(this.pageType == 'Orden'){
            this.createOrder();
        } else if(this.pageType == 'Nota de Credito'){
            this.createCreditNote();
        } else if(this.pageType == 'Nota de Debito'){
            this.createDebitNote();
        }
    }

    createOrder() {
        var createUpdateOrder = new CreateUpdateOrderDto();
        createUpdateOrder.customerId = this.order.customerId;
        createUpdateOrder.state = DocumentState.Created;
        createUpdateOrder.items = this.order.items.map(x => {
            return this.mapDocumentItem(x);
        });
        createUpdateOrder.paymentMethods = this.order.paymentMethods.map(x => {
            return this.mapPaymentMethod(x);
        });


        this._posService.createOrder(createUpdateOrder).then(
            (data) => {
                this.resetOrder();
            },
            (error) => {
                console.log("Pos-Sidebar-Component: Error Creating Order " +
                    JSON.stringify(error)
                );
            }
        )
    }

    mapDocumentItem(orderItem: OrderItemDto) {
        var dto = new CreateUpdateDocumentItemDto();
        dto.productId = orderItem.productId || '',
            dto.name = orderItem.name || '',
            dto.description = orderItem.description || '',
            dto.code = orderItem.code || '',
            dto.salePrice = orderItem.salePrice || 0,
            dto.taxes = orderItem.taxes || 0,
            dto.discount = orderItem.discount || 0,
            dto.quantity = orderItem.quantity || 0,
            dto.totalItem = (orderItem.quantity * orderItem.salePrice) + (orderItem.quantity * orderItem.salePrice * orderItem.taxes) - (orderItem.quantity * orderItem.salePrice * orderItem.discount) || 0;

        return dto;
    }

    mapPaymentMethod(payment: PaymentMethodDto) {
        var dto = new CreateUpdatePaymentMethodDto();
        dto.amount = payment.amount;
        dto.paymentMethodTypeId = payment.paymentMethodTypeId;

        return dto;
    }

    createCreditNote() {
        var dto = new CreateUpdateCreditNoteDto();
        dto.orderId = this.order.id;
        dto.state = DocumentState.Created;
        dto.customerId = this.order.customerId;
        dto.items = this.order.items.map(x => {
            return this.mapDocumentItem(x);
        });

        this._posService.createCreditNote(dto).then(
            (data) => {
                this.resetOrder();
            },
            (error) => {
                console.log("Pos-Sidebar-Component: Error Creating Credit Note " +
                    JSON.stringify(error)
                );
            }
        )
    }

    createDebitNote() {
        var dto = new CreateUpdateDebitNoteDto();
        dto.orderId = this.order.id;
        dto.state = DocumentState.Created;
        dto.customerId = this.order.customerId;
        dto.items = this.order.items.map(x => {
            return this.mapDocumentItem(x);
        });

        this._posService.createDebitNote(dto).then(
            (data) => {
                this.resetOrder();
            },
            (error) => {
                console.log("Pos-Sidebar-Component: Error Creating Debit Note " +
                    JSON.stringify(error)
                );
            }
        )
    }
}
