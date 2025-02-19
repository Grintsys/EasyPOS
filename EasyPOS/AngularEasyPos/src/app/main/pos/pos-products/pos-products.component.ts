import {
    Component,
    ElementRef,
    Input,
    OnChanges,
    Output,
    SimpleChanges,
    ViewChild,
    ViewEncapsulation,
    EventEmitter
} from "@angular/core";

import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";

import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";

import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";
import { MatTableDataSource } from "@angular/material/table";
import { OrderItemDto, TaxesDto } from "app/main/orders/order.model";
import { SharedService } from "app/shared.service";
import { Subscription } from "rxjs";
import { ProductDto } from "app/main/products/product.model";
import { PosService } from "../pos.service";

@Component({
    selector: "pos-products",
    templateUrl: "./pos-products.component.html",
    styleUrls: ["./pos-products.component.scss"],
    encapsulation: ViewEncapsulation.None,
})
export class PosProductsComponent implements OnChanges {
    dataSource = new MatTableDataSource();
    displayedColumns: string[] = [
        "code",
        "name",
        "quantity",
        "salePrice",
        "tax",
        "discount",
        "total",
        "options",
    ];

    @ViewChild(MatPaginator, { static: true })
    paginator: MatPaginator;

    @ViewChild(MatSort, { static: true })
    sort: MatSort;

    @ViewChild("filter", { static: true })
    filter: ElementRef;

    @Input() orderItems: OrderItemDto[];
    @Output() newOrderItemsEvent = new EventEmitter<OrderItemDto[]>();
    currency: string= '';
    taxesList: TaxesDto[];
    subscription: Subscription;
    productList: ProductDto[];
    pageType: string;
    food: any = ['Pizza', 'Sandwich'];
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _sharedService: SharedService,
        public _posService: PosService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.dataSource = new MatTableDataSource();
        this.orderItems = [];
        this.taxesList = [];

        this.subscription = _sharedService.posPageType$.subscribe(
            type => {
                this.pageType = type;
            }
        );

        this.subscription = _sharedService.productList$.subscribe(
            list => {
                this.productList = list;
            }
        );
    }

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;

        this.getConfigList('Impuestos');
        this.getConfigList('Moneda');
    }


    getConfigList(filter: string) {
        this._posService.getConfList(filter).then(
            (d) => {
                if (filter == 'Moneda') {
                    this.currency = JSON.parse(d[0].value).Currency;
                } else if (filter == 'Impuestos') {
                    this.taxesList = JSON.parse(d[0].value);

                }
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }

    selectedTaxChange(value: any) {
        console.log(value);
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes.orderItems && changes.orderItems.currentValue) {
            this.orderItems = changes.orderItems.currentValue.map(x => {
                if (this.productList != undefined) {
                    var prodIndex = this.productList.findIndex(y => y.id == x.productId);

                    if (prodIndex != -1 && x.quantity > this.productList[prodIndex].inventory) {
                        x.quantity = this.productList[prodIndex].inventory;
                    }
                }

                return x;
            })
            this.dataSource = new MatTableDataSource(this.orderItems);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        }
    }

    increaseOrderItem(orderItemId: string) {
        var index = this.productList.findIndex(x => x.id == orderItemId);
        var orderItemIndex = this.orderItems.findIndex(x => x.productId == orderItemId);

        if (this.productList[index].inventory > this.orderItems[orderItemIndex].quantity) {
            this.orderItems[orderItemIndex].quantity++;
            this.orderItems[orderItemIndex].totalItem = this.calculateTotalItem(this.orderItems[orderItemIndex]);
        }

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    decreaseOrderItem(orderItemId: string) {
        this.orderItems.map(x => {
            if (x.productId == orderItemId && x.quantity > 1) {
                x.quantity--;
                x.totalItem = this.calculateTotalItem(x);
            }
        });

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    increaseDiscount(orderItemId: string) {
        var orderItemIndex = this.orderItems.findIndex(x => x.productId == orderItemId);

        if (this.orderItems[orderItemIndex].discount < 5) {
            this.orderItems[orderItemIndex].discount++;
            this.orderItems[orderItemIndex].totalItem = this.calculateTotalItem(this.orderItems[orderItemIndex]);
        }

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    decreaseDiscount(orderItemId: string) {
        var orderItemIndex = this.orderItems.findIndex(x => x.productId == orderItemId);

        if (this.orderItems[orderItemIndex].discount > 0) {
            this.orderItems[orderItemIndex].discount--;
            this.orderItems[orderItemIndex].totalItem = this.calculateTotalItem(this.orderItems[orderItemIndex]);
        }

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    removeOrderItem(orderItemId: string) {
        this.orderItems = this.orderItems.filter(x => x.productId != orderItemId);
        this.dataSource = new MatTableDataSource(this.orderItems);

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    changeDiscount(discount: string, orderItemId: string) {
        if (Number(discount) > 0 && Number(discount) <= 100) {
            this.orderItems.map(x => {
                if (x.productId == orderItemId && x.quantity > 1) {
                    x.discount = Number(discount);
                    x.totalItem = this.calculateTotalItem(x);
                }
            });

            this.newOrderItemsEvent.emit(this.orderItems);
        }
    }

    changeQuantity(quantity: string, orderItemId: string) {
        var index = this.productList.findIndex(x => x.id == orderItemId);
        var orderItemIndex = this.orderItems.findIndex(x => x.productId == orderItemId);

        if (Number(quantity) > 0 && this.productList[index].inventory >= Number(quantity)) {
            this.orderItems[orderItemIndex].quantity = Number(quantity);
            this.orderItems[orderItemIndex].totalItem = this.calculateTotalItem(this.orderItems[orderItemIndex]);
        }

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    calculateTotalItem(x: OrderItemDto) {
        var iva = x ? x.quantity * x.salePrice * 0.15 : 0; //TODO sacar % de config manager
        return x.quantity * x.salePrice + iva - (x.quantity * x.salePrice * (x.discount / 100));
    }
}
