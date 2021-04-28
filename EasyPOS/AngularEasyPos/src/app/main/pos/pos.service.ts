import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { CustomerDto } from "../customers/customer.model";
import { PaymentMethodTypeDto, CreateUpdateOrderDto, OrderDto, CreateUpdateCreditNoteDto, CreateUpdateDebitNoteDto, DebitNoteDto, CreditNoteDto } from "../orders/order.model";
import { CreateUpdateProductWarehouseDto, ProductDto } from "../products/product.model";

@Injectable()
export class PosService implements Resolve<any>{
    baseUrl: string;
    authToken: string;
    routeParams: any;
    onPosChanged: BehaviorSubject<any>;

    constructor(private _httpClient: HttpClient, private router: Router){
        this.onPosChanged = new BehaviorSubject({});
        this.checkSession();
    }

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> | Promise<any> | any {
        this.routeParams = route.params;
        return new Promise<void>((resolve, reject) => {
            Promise.all([this.getDocument()]).then(() => {
                resolve();
            }, reject);
        });
    }

    getDocument(): Promise<any> {
        return new Promise((resolve, reject) => {
            var data = {
                Id: this.routeParams.id,
                Type: this.routeParams.handle,
            };
            this.onPosChanged.next(data);
            resolve(data);
        });
    }

    public getProductList(filter: string): Promise<any> {
        var warehouseId = localStorage.getItem('warehouseId');
        var url = `${this.baseUrl}/product/product-list/${warehouseId}${
            filter != `` ? `?filter=${filter}` : ``
        }`;
        const promise = this._httpClient
            .get<ProductDto[]>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getPaymentMethods(): Promise<any> {
        var url = `${this.baseUrl}/payment-method-type/payment-methods`;
        const promise = this._httpClient
            .get<PaymentMethodTypeDto[]>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public createOrder(data: CreateUpdateOrderDto): Promise<any> {
        var url = `${this.baseUrl}/order`;
        const promise = this._httpClient
            .post<OrderDto>(url, data, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public createCreditNote(data: CreateUpdateCreditNoteDto): Promise<any> {
        var url = `${this.baseUrl}/credit-note`;
        const promise = this._httpClient
            .post<CreditNoteDto>(url, data, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public createDebitNote(data: CreateUpdateDebitNoteDto): Promise<any> {
        var url = `${this.baseUrl}/debit-note`;
        const promise = this._httpClient
            .post<DebitNoteDto>(url, data, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getOrder(orderId: string): Promise<any> {
        var url = `${this.baseUrl}/order/${orderId}`;
        const promise = this._httpClient
            .get<OrderDto>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getCustomer(customerId: string): Promise<any> {
        var url = `${this.baseUrl}/customer/${customerId}`
        const promise = this._httpClient.get<CustomerDto>(url, this.getHttpOptions()).toPromise();
        return promise;
    }

    public updateInventory(dto: CreateUpdateProductWarehouseDto): Promise<any>{
        var url = `${this.baseUrl}/product-warehouse/by-product-and-warehouse-id`;
        debugger;
        const promise = this._httpClient
            .put<CreateUpdateProductWarehouseDto>(url, dto, this.getHttpOptions()).toPromise();
        return promise;
    }

    private getHttpOptions() {
        return {
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                Authorization: this.authToken,
            }),
        };
    }

    private checkSession() {
        this.baseUrl = `${localStorage.getItem("baseUrl")}/api/app`;
        this.authToken = localStorage.getItem("token");

        if (this.authToken == null || this.baseUrl == null) {
            this.router.navigate(["/login"]);
        }
    }
}