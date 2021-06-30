import {
    Component,
    EventEmitter,
    OnDestroy,
    OnInit,
    Output,
} from "@angular/core";
import { Subject, Subscription } from "rxjs";
import { takeUntil } from "rxjs/operators";

import { FuseConfigService } from "@fuse/services/config.service";
import { ProductDto } from "app/main/products/product.model";
import { PosService } from "../pos.service";
import { OrderItemDto } from "app/main/orders/order.model";
import { SharedService } from "app/shared.service";

@Component({
    selector: "search-results",
    templateUrl: "./search-results.component.html",
    styleUrls: ["./search-results.component.scss"],
})
export class SearchResultsComponent implements OnInit, OnDestroy {
    @Output()
    input: EventEmitter<any>;

    @Output()
    newOrderItemEvent = new EventEmitter<OrderItemDto>();

    fuseConfig: any;
    collapsed: boolean;
    selectedWarehouseId: string;
    productList: ProductDto[];
    subscription: Subscription;
    searchFilter: string;

    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseConfigService: FuseConfigService,
        private _posService: PosService,
        private _sharedService: SharedService
    ) {
        this._unsubscribeAll = new Subject();

        this.input = new EventEmitter();

        this.collapsed = true;
        // this.productList = [];
        this.productList = this.dataTemp();

        this.subscription = _sharedService.selectedWarehouseId$.subscribe(
            () => {
                this.getProductList("");
            }
        );

        this.subscription = _sharedService.posProductsSearch$.subscribe(
            data => {
                data != undefined ? this.searchFilter = data : this.searchFilter = '';
                this.getProductList(this.searchFilter);
            }
        );
    }

    ngOnInit(): void {
        // Subscribe to config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
                this.fuseConfig = config;
            });

        this.getProductList("");
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    collapse(): void {
        this.collapsed = true;
    }

    search(event: any): void {
        // this.input.emit(event.target.value);
        this.getProductList(event.target.value);
    }

    getProductList(filter: string) {
        this._posService.getProductList(filter).then(
            (data) => {
                this.productList = data;
                this._sharedService.setProductList(this.productList);
            },
            (error) => {
                console.log("Search-Results-Component: Error Getting Product List " +
                    JSON.stringify(error)
                );
            }
        );
    }

    addOrderItem(newItem: OrderItemDto) {
        this.newOrderItemEvent.emit(newItem);
    }

    dataTemp(): ProductDto[] {
        return [
            {
                id: '1',
                name: 'Filtro de Aceite SUPRAJIT COMPACT',
                description: 'Filtro de Aceite SUPRAJIT COMPACT',
                code: 'SBOF2011',
                salePrice: 82.92,
                taxes: 10,
                isActive: true,
                imageUrl: 'https://multicomer.com/wp-content/uploads/2013/01/12SBOF2011part_720x.jpg',
                inventory: 250,
                productWarehouse: [],
            },
            {
                id: '2',
                name: 'Zapata de freno SUPRAJIT PLSR/DSVR/APCH/GXT/GN125',
                description: 'Zapata de Freno SUPRAJIT PLSR/DSVR/APCH/GXT/GN125',
                code: 'SBOF2011',
                salePrice: 107.89,
                taxes: 50,
                isActive: true,
                imageUrl: 'https://multicomer.com/wp-content/uploads/2021/03/150-SBS0305-ZAPATA-DE-FRENO-SUPRAJIT-PLSR-DSVR-APCH-GXT-GN125-scaled.jpg',
                inventory: 250,
                productWarehouse: [],
            },
            {
                id: '3',
                name: 'Cable de Clutch SUPRAJIT 2013-2019',
                description: 'Cable de Clutch SUPRAJIT 2013-2019',
                code: 'AA191092-SUP',
                salePrice: 233.97,
                taxes: 50,
                isActive: true,
                imageUrl: 'https://multicomer.com/wp-content/uploads/2021/03/24-AA191092-SUP-CABLE-DE-CLUTCH-SUPRAJIT-2013-2019-scaled.jpg',
                inventory: 588,
                productWarehouse: [],
            },
            {
                id: '4',
                name: 'Kit de Cilindro NPC PLSR 135',
                description: 'ZKit de Cilindro NPC PLSR 135',
                code: 'SBOF2011',
                salePrice: 1168.16,
                taxes: 12,
                isActive: true,
                imageUrl: 'https://multicomer.com/wp-content/uploads/2021/01/4652-JE00-16_720x.jpg',
                inventory: 540,
                productWarehouse: [],
            },
            {
                id: '5',
                name: 'Cadena de Tiempo 94L NPC APCH 160/180, 200NS',
                description: 'Cadena de Tiempo 94L NPC APCH 160/180, 200NS',
                code: 'SBOF2011',
                salePrice: 258.22,
                taxes: 20,
                isActive: true,
                imageUrl: null,
                inventory: 365,
                productWarehouse: [],
            },
            {
                id: '6',
                name: 'Cadena de 2 Tiempos',
                description: 'Cadena de 2 Tiempos',
                code: 'SBOF209',
                salePrice: 1254.22,
                taxes: 5,
                isActive: true,
                imageUrl: null,
                inventory: 1865,
                productWarehouse: [],
            },
            {
                id: '7',
                name: 'Cascos reforzados',
                description: 'Color rojo',
                code: 'SBOF2020',
                salePrice: 5598.22,
                taxes: 10,
                isActive: true,
                imageUrl: "https://www.remove.bg/images/remove_image_background.jpg",
                inventory: 200,
                productWarehouse: [],
            }
        ];
    }
}
