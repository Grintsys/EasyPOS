import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import {
    ActivatedRouteSnapshot,
    Resolve,
    Router,
    RouterStateSnapshot,
} from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { CreateUpdateOrderDto, DocumentDto, DocumentItemDto, OrderDto } from "./order.model";
import { ConfigurationDto } from "../configurations/configuration.model";

@Injectable()
export class OrderService implements Resolve<any> {
    baseUrl: string;
    authToken: string;
    routeParams: any;
    order: any;
    onOrderChanged: BehaviorSubject<any>;

    constructor(private _httpClient: HttpClient, private router: Router) {
        // Set the defaults
        this.onOrderChanged = new BehaviorSubject({});
        this.checkSession();
    }

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> | Promise<any> | any {
        this.routeParams = route.params;

        return new Promise<void>((resolve, reject) => {
            Promise.all([this.getOrder()]).then(() => {
                resolve();
            }, reject);
        });
    }

    getOrder(): Promise<any> {
        return new Promise((resolve, reject) => {
            var data = {
                Id: this.routeParams.id,
                Type: this.routeParams.handle,
            };
            this.onOrderChanged.next(data);
            resolve(data);
        });
    }

    public create(data: CreateUpdateOrderDto): Promise<any> {
        var url = `${this.baseUrl}order`;
        const promise = this._httpClient
            .post<OrderDto>(url, data, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public update(orderId: string, data: CreateUpdateOrderDto): Promise<any> {
        var url = `${this.baseUrl}order/${orderId}`;
        const promise = this._httpClient
            .put<OrderDto>(url, data, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public delete(orderId: string): Promise<any> {
        var url = `${this.baseUrl}order/${orderId}`;
        const promise = this._httpClient
            .delete<OrderDto>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public deleteCreditNote(id: string): Promise<any> {
        var url = `${this.baseUrl}credit-note/${id}`;
        const promise = this._httpClient
            .delete<OrderDto>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public deleteDebitNote(id: string): Promise<any> {
        var url = `${this.baseUrl}debit-note/${id}`;
        const promise = this._httpClient
            .delete<OrderDto>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public get(orderId: string): Promise<any> {
        var url = `${this.baseUrl}order/${orderId}`;
        const promise = this._httpClient
            .get<OrderDto>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getList(filter: string): Promise<any> {
        var url = `${this.baseUrl}order/order-list${filter != `` ? `?filter=${filter}` : ``}`;
        const promise = this._httpClient
            .get<DocumentDto<DocumentItemDto>[]>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getDebitNote(id: string): Promise<any> {
        var url = `${this.baseUrl}debit-note/${id}`;
        const promise = this._httpClient
            .get<DocumentDto<DocumentItemDto>[]>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }
    
    public getDebitNoteList(): Promise<any> {
        var url = `${this.baseUrl}debit-note/debit-note-list`;
        const promise = this._httpClient
            .get<DocumentDto<DocumentItemDto>[]>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getOrderItemsByOrderId(orderId: string): Promise<any> {
        var url = `${this.baseUrl}order-item/order-items-by-order-id/${orderId}`;
        const promise = this._httpClient
            .get<OrderDto[]>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getOrderDocumentsByOrderId(orderId: string): Promise<any> {
        var url = `${this.baseUrl}order/order-documents/${orderId}`;
        const promise = this._httpClient
            .get<OrderDto[]>(url, this.getHttpOptions())
            .toPromise();
        return promise;
    }

    public getConfList(filter: string): Promise<any> {
        var url = `${this.baseUrl}configuration-manager/config-list${filter != `` ? `?filter=${filter}` : ``}`;
        const promise = this._httpClient.get<ConfigurationDto[]>(url, this.getHttpOptions()).toPromise();
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
        this.baseUrl = `${localStorage.getItem("baseUrl")}/api/app/`;
        this.authToken = localStorage.getItem("token");

        if (this.authToken == null || this.baseUrl == null) {
            this.router.navigate(["/login"]);
        }
    }
}
