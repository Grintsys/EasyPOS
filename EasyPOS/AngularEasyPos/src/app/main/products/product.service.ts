import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import {
    ActivatedRouteSnapshot,
    Resolve,
    RouterStateSnapshot,
} from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { CreateUpdateProductDto, ProductDto } from "./product.model";

@Injectable()
export class ProductService implements Resolve<any> {
    baseUrl: string = "https://localhost:44339/api/app/product";
    routeParams: any;
    product: any;
    onProductChanged: BehaviorSubject<any>;

    httpOptions: any = {
        headers: new HttpHeaders({
            "Content-Type": "application/json",
            Authorization: "CfDJ8LgWl1xuO5pDiRiK3SLIzjX1aWU4lUFH6TwN9kKYs1B9_kdxVw3zuxEke3MgZX-Exqsq4Oz921JLEz4tCd6eCKb3coajpiAGBW7WMVIsJO03TV9Fx0CgNUYJuS9gTyauP1TnyQdJbUflzCsxVRTJdms",
        }),
    };

    /**
     * Constructor
     *
     * @param {HttpClient} _httpClient
     */
    constructor(private _httpClient: HttpClient) {
        // Set the defaults
        this.onProductChanged = new BehaviorSubject({});
    }

    /**
     * Resolver
     *
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> | Promise<any> | any {
        this.routeParams = route.params;

        return new Promise<void>((resolve, reject) => {
            Promise.all([this.getProduct()]).then(() => {
                resolve();
            }, reject);
        });
    }

    /**
     * Get product
     *
     * @returns {Promise<any>}
     */
    getProduct(): Promise<any> {
        return new Promise((resolve, reject) => {
            var data = {
                Id: this.routeParams.id,
                Type: 'view',
            };
            this.onProductChanged.next(data);
            resolve(data);
        });
    }
    
    public getProductLookup(): Promise<any> {
        const promise = this._httpClient.get<ProductDto[]>(this.baseUrl, this.httpOptions).toPromise();
        return promise;
    }
    
    public get(productId: string): Promise<any> {
        var url = `${this.baseUrl}/${productId}/product`
        const promise = this._httpClient.get<ProductDto>(url, this.httpOptions).toPromise();
        return promise;
    }
    
    public getList(filter: string): Promise<any> {
        var url = `${this.baseUrl}/product-list${filter != `` ? `?filter=${filter}` : ``}`;
        const promise = this._httpClient.get<ProductDto[]>(url, this.httpOptions).toPromise();
        return promise;
    }

    public getListByWarehouse(wareHouseId: string): Promise<any> {
        var url = `${this.baseUrl}/product-list-by-warehouse/${wareHouseId}`
        const promise = this._httpClient.get<ProductDto[]>(url, this.httpOptions).toPromise();
        return promise;
    }
}
