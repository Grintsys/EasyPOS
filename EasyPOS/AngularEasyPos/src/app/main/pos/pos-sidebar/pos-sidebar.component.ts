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
    OrderDto, OrderItemDto, OrderType, PaymentMethodDto
} from "app/main/orders/order.model";
import { PosService } from "../pos.service";
import { CustomerDto } from "app/main/customers/customer.model";
import { takeUntil } from "rxjs/operators";
import { Subject } from "rxjs";
import { Router } from "@angular/router";
import { SharedService } from "app/shared.service";
import { CreateUpdateProductWarehouseDto } from "app/main/products/product.model";

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
    orderType: OrderType;
    pageType: string;
    paymentMethod: CreateUpdatePaymentMethodDto;
    
    selectedOrderType: string;

    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _matDialog: MatDialog,
        public _posService: PosService,
        public _sharedService: SharedService,
        private _router: Router
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this._unsubscribeAll = new Subject();
        this.color = "rgba(223, 196, 0, 0.11)";
        this.order = new OrderDto();
        this.customer = new CustomerDto();
        this.isOrderReady = false;
        this.orderType = OrderType.Ninguno;
        this.paymentMethod = new CreateUpdatePaymentMethodDto;
    }

    ngOnInit(): void {
        // Subscribe to update customer on changes
        this._posService.onPosChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (this._router.url == "/pos") {
                    this.setOrderType('Contado');
                    
                    this.pageType = "Orden";
                } else if (this._router.url == "/debit-note") {
                    this.pageType = "Nota de Debito";
                } else if (data.Type == "nota-credito") {
                    this.pageType = "Nota de Credito";
                }
            });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    openPaymentMethodDialog(): void {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.panelClass = "payment-method-dialog";
        dialogConfig.data = {
            paymentMethdosData: this.paymentMethod,
            discount: this.order.discount,
            subtotal: this.order.subTotal,
            taxes: this.order.isv,
        };

        this.dialogRef = this._matDialog.open(PaymentMethodsComponent, dialogConfig);

        this.dialogRef.afterClosed().subscribe((pm) => {

            if (pm != undefined) {
                this.paymentMethod = pm;
                this.order.paymentAmount = this.getOrderPaymentAmount(this.paymentMethod);
            }

            this.validateOrder();
        });
    }

    getOrderPaymentAmount(payment: any) {
        var paymentAmount = 0;
        if (payment.cash != undefined)
            paymentAmount += payment.cash.total;

        if (payment.wireTransfer != undefined)
            paymentAmount += payment.wireTransfer.total;

        if (payment.creditDebitCard != undefined)
            paymentAmount += payment.creditDebitCard.total;

        if (payment.bankChecks != undefined)
            paymentAmount += payment.bankChecks.map(a => a.total).reduce((a, c) => a + c, 0);

        return paymentAmount;
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
                this.order.customerCode = this.customer.code;
                this.order.customerId = this.customer.id;
                this.order.customerName = this.customer.firstName + ' ' + this.customer.lastName;
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
                return a + (value.salePrice * value.quantity) * (value.discount / 100);
            }, 0);
            this.order.isv = this.order.items.reduce(function (a, value) {
                if(value.taxes){
                    return a + (value.salePrice * value.quantity * 0.15);
                }
                return a;
            }, 0);

            this.order.total = this.order.items.reduce(function (a, value) {
                return a + value.totalItem;
            }, 0);
            this.validateOrder();
        }
    }

    validateOrder() {
        if (this.orderType == OrderType.Contado && this.order.customerId != undefined
            && this.order.items.length > 0 && this.getOrderPaymentAmount(this.paymentMethod) > 0) {
            this.isOrderReady = true;
        }
        else if (this.orderType == OrderType.Credito && this.order.customerId != undefined
            && this.order.items.length > 0) {
            this.isOrderReady = true;
        }
        else if (this.orderType == OrderType.Ninguno && this.pageType == 'Orden') {
            this.isOrderReady = false;
        }
        else if (this.pageType == 'Nota de Debito' && this.order.customerId != undefined
            && this.order.items.length > 0) {
            this.isOrderReady = true;
        }
        else if (this.pageType == 'Nota de Credito') {
            this.isOrderReady = true;
        }
        else {
            this.isOrderReady = false;
        }
    }

    resetOrder() {
        if (this.pageType == 'Nota de Credito') {
            this.pageType = 'Orden';
            this._router.navigate([`/document/${this.order.id}/view`]);

        }
        this.order = new OrderDto();
        this.customer = new CustomerDto();
        this.orderType = OrderType.Contado;
        this.newOrderEvent.emit(this.order);
    }

    create() {
        if (this.pageType == 'Orden') {
            this.createOrder();
        } else if (this.pageType == 'Nota de Credito') {
            this.createCreditNote();
        } else if (this.pageType == 'Nota de Debito') {
            this.createDebitNote();
        }
    }

    setOrderType(orderType: string) {
        if (orderType == 'Contado') {
            this.orderType = OrderType.Contado;
            this.selectedOrderType = 'contado';
        } else if (orderType == 'Credito') {
            this.orderType = OrderType.Credito;
            this.selectedOrderType = 'credito';
        }
        this.validateOrder();
    }

    public onValChange(val: string) {
        this.selectedOrderType = val;
    }
    
    createOrder() {
        var createUpdateOrder = new CreateUpdateOrderDto();
        createUpdateOrder.customerId = this.order.customerId;
        createUpdateOrder.customerCode = this.order.customerCode;
        createUpdateOrder.customerName = this.order.customerName;
        createUpdateOrder.warehouseCode = localStorage.getItem("warehouseCode");
        createUpdateOrder.state = DocumentState.Creada;
        createUpdateOrder.orderType = this.orderType;
        createUpdateOrder.items = this.order.items.map(x => {
            return this.mapDocumentItem(x);
        });
        createUpdateOrder.paymentMethods = this.paymentMethod;

        this._posService.createOrder(createUpdateOrder).then(
            (data) => {
                this.updateInventory();
                this.resetOrder();
            },
            (error) => {
                console.log("Pos-Sidebar-Component: Error Creating Order " +
                    JSON.stringify(error)
                );
            }
        )
    }

    updateInventory() {
        var productWarehouseList: CreateUpdateProductWarehouseDto[] = [];

        this.order.items.forEach(
            x => {
                var dto = new CreateUpdateProductWarehouseDto();
                dto.productId = x.productId;
                dto.warehouseId = localStorage.getItem("warehouseId");
                dto.inventory = x.quantity;
                productWarehouseList.push(dto);
            }
        );

        this._posService.updateInventory(productWarehouseList).then(
            () => {
                //Hacemos esto para actualizar la lista de prouductos despues de actualizar el inventario
                this._sharedService.updateWarehouse(localStorage.getItem("warehouseId"));
            },
            (error) => {
                console.log("Error: pos-sidebar-component->UpdateInventory: " +
                    JSON.stringify(error)
                );
            }
        );
    }

    createCreditNote() {
        var dto = new CreateUpdateCreditNoteDto();
        dto.orderId = this.order.id;
        dto.state = DocumentState.Creada;
        dto.customerId = this.order.customerId;
        dto.customerCode = this.order.customerCode;
        dto.customerName = this.order.customerName;
        dto.warehouseCode = localStorage.getItem("warehouseCode");
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
        dto.state = DocumentState.Creada;
        dto.customerId = this.order.customerId;
        dto.customerCode = this.order.customerCode;
        dto.customerName = this.order.customerName;
        dto.warehouseCode = localStorage.getItem("warehouseCode");
        dto.items = this.order.items.map(x => {
            return this.mapDocumentItem(x);
        });

        this._posService.createDebitNote(dto).then(
            (data) => {
                this.updateInventory();
                this.resetOrder();
            },
            (error) => {
                console.log("Pos-Sidebar-Component: Error Creating Debit Note " +
                    JSON.stringify(error)
                );
            }
        )
    }

    removePaymentMethod(id: string) {

    }


    mapDocumentItem(orderItem: OrderItemDto) {
        var dto = new CreateUpdateDocumentItemDto();
        dto.productId = orderItem.productId || '',
            dto.name = orderItem.name || '',
            dto.description = orderItem.description || '',
            dto.code = orderItem.code || '',
            dto.salePrice = orderItem.salePrice || 0,
            dto.taxes = orderItem.taxes || false,
            dto.discount = orderItem.discount || 0,
            dto.quantity = orderItem.quantity || 0,
            dto.totalItem = orderItem.totalItem || 0

        return dto;
    }
}
